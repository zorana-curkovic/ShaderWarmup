using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualWarmup : MonoBehaviour
{
    [Range(1,15)]
    public int warmupAfter;
    
    public GameObject WarmupCamera;
    public TMPro.TMP_Text field;

    private bool warmupOn;
    private Coroutine warmupCo;
    
    void OnAwake()
    {
        WarmupCamera.SetActive(false);
        warmupCo = null;
    }

    void Start()
    {
        // but warmupOn has to be set up from script
        if (warmupOn == true && warmupCo == null)
            warmupCo = StartCoroutine(WarmupCo());
    }

    public void ToggleWarmup(bool _enabled)
    {
        bool lastState = warmupOn;
        warmupOn = _enabled;


        if (warmupOn == true && warmupCo == null)
            warmupCo = StartCoroutine(WarmupCo());
        
    }
    
    IEnumerator WarmupCo()
    {
        if (warmupOn) {
            
            yield return new WaitForSeconds(warmupAfter);
            WarmupCamera.SetActive(true);
            
        
            yield return new WaitForEndOfFrame();
            WarmupCamera.SetActive(false);
            field.text = "Warmup - Done";

            warmupCo = null;
        }
    }

}
