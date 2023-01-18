We are prewarming orange material shader which is URP Lit
When prewarm is triggered (UI shows Prewarm done) profiler will indicate CreateGPUProgram marker on Render Thread - that's when shader is loaded for the first time
When spheres with orange material comes into the view of camera (after toggling rotation),
CreateGPUProgram should not appear. If it appears that mean that prewarm wasn't successful.

Manual warmup scene will enable WarmupCameraAndQuad object containing camera looking at quad with orange material

Automatic - New API for warming up shaders and shaders from SVCs will do this step programmatically 

Note: Profiling should be always done on running build - not in the editor :)