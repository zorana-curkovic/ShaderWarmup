using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;
using System.Collections.Generic;


public class WarmupRenderFeature : ScriptableRendererFeature
{
    class WarmupRenderPass : ScriptableRenderPass
    {
        int temporaryBufferID;
        private RenderTargetIdentifier _warmupBuffer;
        FilteringSettings m_FilteringSettings;
        WarmupRenderFeature.PassSettings passSettings;
        List<ShaderTagId> m_ShaderTagIdList = new List<ShaderTagId>();
        RenderStateBlock m_RenderStateBlock;
        public WarmupRenderPass( WarmupRenderFeature.PassSettings passSettings)
        {
            this.passSettings = passSettings;
            temporaryBufferID = Shader.PropertyToID("WarmupBuffer");
            m_FilteringSettings = new FilteringSettings(RenderQueueRange.all, passSettings.LayerMask);
            m_RenderStateBlock = new RenderStateBlock();

            m_ShaderTagIdList.Add(new ShaderTagId("UniversalForward"));

        }
        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in a performant manner.
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        
            //could downscale things here if you wanted, maybe depending on performance might be okay 

            ConfigureInput(ScriptableRenderPassInput.Depth);
            cmd.GetTemporaryRT(temporaryBufferID, descriptor, FilterMode.Bilinear);
            _warmupBuffer = new RenderTargetIdentifier(temporaryBufferID);
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();

            //I have no idea what I"m doing ᕕ( ᐛ )ᕗ

            DrawingSettings drawSettings;
            drawSettings = CreateDrawingSettings(m_ShaderTagIdList, ref renderingData, SortingCriteria.CommonOpaque);
            context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref m_FilteringSettings, ref m_RenderStateBlock);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if(cmd == null) throw new ArgumentNullException("cmd");
            cmd.ReleaseTemporaryRT(temporaryBufferID);
        }
    }

    [System.Serializable]
    public class PassSettings {
        //when to inject the pass
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
        //name of the texture you can grab in shaders
        public string TextureName = "_CustomRenderBuffer";
        //only renders objects in the layers below
        public LayerMask LayerMask;
    }
    public PassSettings passSettings = new PassSettings();
    WarmupRenderPass m_WarmupRenderPass;

    /// <inheritdoc/>
    public override void Create()
    {
        
        m_WarmupRenderPass = new WarmupRenderPass(passSettings);
        
        // Configures where the render pass should be injected.
        m_WarmupRenderPass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(m_WarmupRenderPass);
    }
}


