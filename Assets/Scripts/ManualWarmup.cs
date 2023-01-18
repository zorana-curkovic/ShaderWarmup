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
    private float timeElapsed;

    void Awake()
    {
        WarmupCamera.SetActive(false);
        warmupCo = null;
        field.text = $"Warmup - {warmupAfter}";
    }

    void Start()
    {
        // but warmupOn has to be set up from script
        if (warmupOn == true && warmupCo == null)
        {
            timeElapsed = 0;
            warmupCo = StartCoroutine(WarmupCo());
        }
    }

    public void ToggleWarmup(bool _enabled)
    {
        bool lastState = warmupOn;
        warmupOn = _enabled;


        if (warmupOn == true && warmupCo == null)
        {
            timeElapsed = 0;
            warmupCo = StartCoroutine(WarmupCo());
        }

    }
    
    IEnumerator WarmupCo()
    {
        if (warmupOn) {
            
            while (timeElapsed < warmupAfter)
            {
                field.text = $"Warmup - {warmupAfter - timeElapsed}";

                yield return new WaitForSeconds(1);
                timeElapsed += 1;
            }
            WarmupCamera.SetActive(true);
        
            yield return new WaitForEndOfFrame();
            WarmupCamera.SetActive(false);
            field.text = "Warmup - Done";

        }
        warmupCo = null;
    }

}
