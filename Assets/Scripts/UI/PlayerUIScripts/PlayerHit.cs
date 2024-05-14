using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHit : MonoBehaviour
{
    // Added these for post effects (post-processing) 
    [SerializeField] Volume postFXVolume;
    [SerializeField] private AnimationCurve flashSpeedCurve;
    [SerializeField] private float showDuration;
    [SerializeField] private GameObject halfHealthVisual;

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

            pfxVignette.intensity.value = Mathf.Clamp(alphaFadeIn, 0, PlayerBase.instance.Health.RevPercent * 0.5f);

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

    public void ToggleHalfHealthVisual(bool _status)
    {
        halfHealthVisual.SetActive(_status);
    }
}
