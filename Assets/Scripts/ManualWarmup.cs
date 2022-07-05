using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualWarmup : MonoBehaviour
{
    public GameObject WarmupCamera;
    public TMPro.TMP_Text field;
    [Range(1,15)]
    public int warmupAfter;
    
    void OnAwake()
    {
        WarmupCamera.SetActive(false);
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(warmupAfter);
        WarmupCamera.SetActive(true);

        yield return new WaitForEndOfFrame();
        WarmupCamera.SetActive(false);
        field.text = "Warmup - Done";
    }

}
