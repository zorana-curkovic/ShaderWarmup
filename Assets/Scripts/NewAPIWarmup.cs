using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UIElements;

public class NewAPIWarmup : MonoBehaviour
{
    [Range(1,15)]
    public int warmupAfter;
    [Tooltip("Debug - List descriptors in console")]
    public bool debug;
    public Mesh mesh;
    [Tooltip("Just for simple shaders (for multiple variants use SVC below)")]
    public Shader justShader;
    [Tooltip("For uber shaders like URP Lit")]
    public ShaderVariantCollection shaderVariantCollection;
    [Tooltip("For uber shaders like URP Lit")]
    public Shader shaderFromVariantCollection;
    public TMPro.TMP_Text field;
    private ShaderWarmupSetup setup;
    private bool warmupOn;
    private Coroutine warmupCo;
    private float timeElapsed;
    
    void Awake()
    {
        warmupCo = null;
        field.text = $"Warmup - {warmupAfter}";
    }
    void Start()
    {
        // but warmupOn has to be set up from script
        if (warmupOn && warmupCo == null &&
            (mesh != null && (justShader != null ||
                              (shaderVariantCollection != null && shaderFromVariantCollection != null))))
        {
            timeElapsed = 0;
            warmupCo = StartCoroutine(WarmupCo());
        }
    }

    public void ToggleWarmup(bool _enabled)
    {
        warmupOn = _enabled;

        if (warmupOn && warmupCo == null && 
            (mesh != null && (justShader != null ||(shaderVariantCollection != null && shaderFromVariantCollection != null))))
        {
            timeElapsed = 0;
            warmupCo = StartCoroutine(WarmupCo());
        }
    }
    
    IEnumerator WarmupCo()
    {
        VertexAttributeDescriptor[] vdescriptor = mesh.GetVertexAttributes();
        
        ShaderWarmupSetup setup = new ShaderWarmupSetup();
        setup.vdecl = vdescriptor;

        while (timeElapsed < warmupAfter)
        {
            field.text = $"Warmup - {warmupAfter - timeElapsed}";

            yield return new WaitForSeconds(1);
            timeElapsed += 1;
        }

        if (warmupOn)
        {
            field.text = "Warmup - Done";
            if (justShader != null)
                ShaderWarmup.WarmupShader(justShader, setup);
            if (shaderVariantCollection != null)
                ShaderWarmup.WarmupShaderFromCollection(shaderVariantCollection, justShader, setup);
        }

        if (debug)
        {
            Debug.Log("V descriptor =");
            for (int i = 0; i < vdescriptor.Length; ++i)
                Debug.Log(vdescriptor[i].attribute.ToString());
        }

        warmupCo = null;
    }
    
}
