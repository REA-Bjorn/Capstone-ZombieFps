using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    // Image to fade and curve to fade with
    public Image img;
    public AnimationCurve curve;

    // Fades in on start
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Fades out to the designated scene
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    // Changes the alpha of the fade image over time (positive)
    IEnumerator FadeIn()
    {
        float t = 2f;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        img.color = new Color(0f, 0f, 0f, 0);
    }

    // Changes the alpha of the fade image over time (negative)
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        img.color = new Color(0f, 0f, 0f, 255);

        GameManager.Instance.UnPauseGame();
        SceneManager.LoadScene(scene);
    }
}