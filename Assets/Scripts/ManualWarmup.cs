using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualWarmup : MonoBehaviour
{
    public bool warmupOn;
    [Range(1,15)]
    public int warmupAfter;
    
    public GameObject WarmupCamera;
    public TMPro.TMP_Text field;

    
    void OnAwake()
    {
        WarmupCamera.SetActive(false);
    }
    
    IEnumerator Start()
    {
        if (warmupOn) {
            
            yield return new WaitForSeconds(warmupAfter);
            WarmupCamera.SetActive(true);
            
        
            yield return new WaitForEndOfFrame();
            WarmupCamera.SetActive(false);
            field.text = "Warmup - Done";
        
        
        }
    }

}
