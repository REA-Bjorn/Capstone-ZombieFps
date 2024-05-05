using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] private AudioSource uiSFX;
    [SerializeField] private AudioClip hoverSFX;
    [SerializeField] private AudioClip clickSFX;

    public void PlayUIHover()
    {
        uiSFX.PlayOneShot(hoverSFX);
    }

    public void PlayUIClick()
    {
        uiSFX.PlayOneShot(clickSFX);
    }

}
