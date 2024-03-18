using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] Image vignette;

    private IEnumerator FadeIn()
    {
        vignette.color = Color.Lerp(Color.clear, Color.red, Time.deltaTime);
        yield return new WaitForSeconds(0.25f);
    }

    public void Active()
    {
       
    }

    public void inActive()
    {

    }

    void Start()
    {

    }

}
