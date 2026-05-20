# WebGL port implementation plan for TKA game

Goal: ship a playable WebGL/WASM build using the most up‑to‑date supported MonoGame web backend, while keeping the shared game logic intact and adapting audio/shaders/input to browser constraints.

### Target Backend (Most Up-to-Date Web Support)
- **Primary choice**: Use the stable MonoGame WebAssembly (WebGL2) backend for .NET 8 browser‑wasm, pinned to the latest recommended stable MonoGame toolchain (currently 3.8.4.1 at implementation time).
- **Rationale**: Prefer official/stable packages and the most actively maintained web backend at implementation time.
- **Fallback**: If the recommended package shifts, update references and MGCB platform to match the current stable backend.

### Action Plan (Expanded)
1. **Decouple Core from DesktopGL**: remove `MonoGame.Framework.DesktopGL` reference from `Helicopter.Core.csproj` and move it to each platform project.
2. **Add a Web project**: create `Helicopter.Web` targeting `net8.0-browser` and reference `Helicopter.Core`.
3. **Wire Web entrypoint**: add a web `Program.cs` entry using the MonoGame WASM host to run `Game1`.
4. **Create Web MGCB**: add `Helicopter.Web.mgcb` with `/platform:WebGL` (or backend‑specific platform) and `/profile:Reach`.
5. **Rebuild shaders for WebGL**: compile all `.fx` for WebGL; remove prebuilt DesktopGL XNBs and generate WebGL variants.
6. **Audio conversion**: convert `Music/*.wma` to `.ogg` or `.mp3`; update `SongManager` to load web formats.
7. **Replace XACT**: remove XACT usage (`.xgs/.xwb/.xsb`) and switch to `SoundEffect` / `SoundEffectInstance` for SFX.
8. **Browser audio gate (required for feature phase)**: add “Tap/Click to Start” gate so audio starts after user interaction.
9. **Input adapter**: add a pointer adapter for Web (mouse → touch) in `InputState` or a web‑specific input wrapper.
10. **Platform flags**: add `IsWeb = OperatingSystem.IsBrowser()` and fold into `IsOpenGL` assumptions.
11. **Packaging**: output `wwwroot/Content` and ensure the content build step copies assets to the web host.
12. **Smoke test**: run Web build in a local dev server and verify input/audio/shader fallbacks.

### Required Adjustments (Explicit)
- **Project structure**: create `Helicopter.Web` and reference `Helicopter.Core`.
- **Core project**: make `Helicopter.Core` platform‑agnostic by removing platform‑specific package references.
- **Audio**: convert to `.ogg` or `.mp3`, add a required click/tap-to-start browser interaction gate before gameplay audio, and replace XACT.
- **Shaders**: remove DesktopGL XNBs and compile `.fx` against WebGL; disable or fallback on failures.
- **Input**: map mouse/touch events to in‑game touch semantics.
- **Platform checks**: add `IsWeb` and treat web as OpenGL.

### Shader WebGL Support List (Audit + Dummy File)
- **Audit list file**: `unsupported-shaders.md`
- **Custom effects to validate**: `circles.fx`, `drunk.fx`, `flip.fx`, `Negative.fx`, `outline.fx`, `shakeblur.fx`, `shakezigzag.fx`, `wave.fx`
- **Prebuilt DesktopGL XNBs (not WebGL‑safe)**: `Effects/effect0.xnb`–`Effects/effect4.xnb`
- **Rule**: keep all effects enabled; if an effect fails on WebGL, unload/disable it at runtime and log it for later fixes.

### Open Questions
- Confirm the recommended MonoGame WebAssembly package name at implementation time.
