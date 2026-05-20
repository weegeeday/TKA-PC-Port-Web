# WebGL Shader Audit - Placeholder List

These effects are custom .fx shaders that must be validated against the WebGL/WASM backend. Until they compile cleanly with the WebGL profile, treat them as unsupported and replace with no-op or fallback effects.

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
- If a shader fails WebGL compilation, replace it with a no-op shader and/or disable the effect in `Game1`.
