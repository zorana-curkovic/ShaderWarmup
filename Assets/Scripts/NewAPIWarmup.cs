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
    public bool debug;
    public Mesh mesh;
    public Shader shader;
    public TMPro.TMP_Text field;
    private ShaderWarmupSetup setup;
    private bool warmupOn;
    private Coroutine warmupCo;

    void OnAwake()
    {
        warmupCo = null;
    }
    void Start()
    {
        
    }

    public void ToggleWarmup(bool _enabled)
    {
        warmupOn = _enabled;

        if (warmupOn && warmupCo == null)
        {
            warmupCo = StartCoroutine(WarmupCo());
            
        }
    
    
    }
    
    IEnumerator WarmupCo()
    {
        VertexAttributeDescriptor[] vdescriptor = mesh.GetVertexAttributes();
        
        ShaderWarmupSetup setup = new ShaderWarmupSetup();
        setup.vdecl = vdescriptor;
        
        yield return new WaitForSeconds(warmupAfter);
        if (warmupOn)
        {
            field.text = "Warmup - Done";
            ShaderWarmup.WarmupShader(shader, setup);
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
