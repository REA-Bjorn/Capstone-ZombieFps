using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHit : MonoBehaviour
{
    // Added these for post effects (post-processing) 
    [SerializeField] Volume postFXVolume;
    [SerializeField] private AnimationCurve flashSpeedCurve;
    [SerializeField] float showDuration;

    private Vignette pfxVignette;
    private float tmpDamageFlash;
    private bool currentlyDamaged;

    void Start()
    {
        tmpDamageFlash = showDuration;
        postFXVolume?.profile.TryGet(out pfxVignette);
        Disabler();
    }

    private IEnumerator DisplayVignette()
    {
        currentlyDamaged = true;
        pfxVignette.intensity.value = 0f;

        while (tmpDamageFlash > 0f)
        {
            tmpDamageFlash -= Time.deltaTime;

            float alphaFadeIn = flashSpeedCurve.Evaluate(tmpDamageFlash);

            pfxVignette.intensity.value = alphaFadeIn;

            yield return 0;
        }
        tmpDamageFlash = showDuration;
        pfxVignette.intensity.value = 0f;
        currentlyDamaged = false;
    }

    private void VignetteFlash()
    {
        if (!currentlyDamaged && isActiveAndEnabled)
        {
            StartCoroutine(DisplayVignette());
        }
    }

    public void Active()
    {
        VignetteFlash();
    }

    public void Disabler()
    {
        pfxVignette.intensity.value = 0f;
    }

    public void PlayerDiedVignette()
    {
        pfxVignette.intensity.value = 1.0f;
    }
}
