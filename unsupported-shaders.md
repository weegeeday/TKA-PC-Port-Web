# WebGL Shader Audit - Placeholder List

These effects are custom .fx shaders that must be validated against the WebGL/WASM backend. Keep all shaders enabled by default, and only unload/disable specific effects if they fail on WebGL at runtime.

## Custom Effects to Validate
- `circles.fx`
- `drunk.fx`
- `flip.fx`
- `Negative.fx`
- `outline.fx`
- `shakeblur.fx`
- `shakezigzag.fx`
- `wave.fx`

## Prebuilt DesktopGL XNBs (Not WebGL-Safe)
- `Effects/effect0.xnb`
- `Effects/effect1.xnb`
- `Effects/effect2.xnb`
- `Effects/effect3.xnb`
- `Effects/effect4.xnb`

## Notes
- Any XNBs built for DesktopGL must be rebuilt using the WebGL/WASM content pipeline.
- If a shader fails WebGL compilation/runtime, log it in this file and unload/disable only that effect in `Game1`.
