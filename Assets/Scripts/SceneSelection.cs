using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
    public void LoadManual()
    {
        SceneManager.LoadScene("Scenes/WarmupManual");
    }

    public void LoadNewAPI()
    {
        SceneManager.LoadScene("NewAPIMetalWarmup");

    }
    
    public void LoadStart()
    {
        SceneManager.LoadScene("Scenes/StartScene");

    }
}
