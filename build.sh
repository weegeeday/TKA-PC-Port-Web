#!/bin/bash
# Exit on any error
set -e

# Define directories
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
DOTNET_DIR="$SCRIPT_DIR/.dotnet-sdk"
PUBLISH_DIR="$SCRIPT_DIR/dist"

echo "=== System Info ==="
uname -a
echo "==================="

echo "Installing .NET SDK 8.0..."
# Download official Microsoft .NET installation script
curl -sSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh
chmod +x dotnet-install.sh

# Install .NET SDK to a local subdirectory to avoid permission issues and isolate the install
./dotnet-install.sh --channel 8.0 --install-dir "$DOTNET_DIR" --no-path

# Clean up the installer script
rm dotnet-install.sh

# -------------------------------------------------
# Install the wasm-tools workload (required for Blazor WASM)
# -------------------------------------------------
"$DOTNET_DIR/dotnet" workload install wasm-tools --skip-manifest-update

# Configure environment variables for the current session
export DOTNET_ROOT="$DOTNET_DIR"
export PATH="$DOTNET_DIR:$PATH"

echo "=== Verified dotnet installation ==="
dotnet --version
echo "===================================="

echo "Building and publishing Helicopter.Web..."
# Publish the project with Release configuration to the output directory
dotnet publish "$SCRIPT_DIR/Helicopter.Web/Helicopter.Web.csproj" -c Release -o "$PUBLISH_DIR"

# -------------------------------------------------
# Optional: copy a favicon so browsers don’t 404
# -------------------------------------------------
if [ -f "$SCRIPT_DIR/Helicopter.Core/icon.ico" ]; then
  cp "$SCRIPT_DIR/Helicopter.Core/icon.ico" "$PUBLISH_DIR/wwwroot/favicon.ico"
fi

echo "===================================="
echo "Publish successful!"
echo "Web files are located in: $PUBLISH_DIR/wwwroot"
echo "Configure your Cloudflare Pages deployment to use:"
echo "  - Build command: ./build.sh"
echo "  - Build output directory: dist/wwwroot"
echo "===================================="
