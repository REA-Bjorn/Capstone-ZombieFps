using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenLoader : MonoBehaviour
{
    public void TriggerFunc()
    {
        SceneManager.LoadScene("MainMenuLevel");
    }
}
