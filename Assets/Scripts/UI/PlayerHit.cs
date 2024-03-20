using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] Image vignette;

    private float tmpDamageFlash;

    private bool currentlyDamaged;

    [SerializeField] private AnimationCurve flashSpeedCurve;

    [SerializeField] float showDuration;

    void Start()
    {
        tmpDamageFlash = showDuration;

        Disabler();
    }

    private IEnumerator DisplayVignette()
    {
        if(!currentlyDamaged)
        {
            currentlyDamaged = true;

            vignette.gameObject.SetActive(true);

            while(tmpDamageFlash > 0f)
            {
                tmpDamageFlash -= Time.deltaTime;

                float alphaFadeIn = flashSpeedCurve.Evaluate(tmpDamageFlash);

                vignette.color = new Color(255.0f, 0f, 0f, alphaFadeIn);

                yield return 0;
            }
            tmpDamageFlash = showDuration;
            vignette.gameObject.SetActive(false);
            currentlyDamaged = false;
        }
        else if (currentlyDamaged)
        {
            tmpDamageFlash += Time.deltaTime;
        }
    }

    private void VignetteFlash()
    {
        vignette.gameObject.SetActive(true);
        if (vignette.gameObject.activeInHierarchy)
        {
            StartCoroutine(DisplayVignette());
        }
    }

    public void Active()
    {
       Invoke("VignetteFlash", 0.1f);
    }

    public void inActive()
    {
        CancelInvoke("DisplayVignette");
    }

    public void Disabler()
    {
        vignette.gameObject.SetActive(false);
    }
}
