using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip reloadSFX;
    [SerializeField] private AudioClip holsterSFX;
    [SerializeField] private AudioClip unholsterSFX;

    public void PlayShoot()
    {
        if (source != null && shootSFX != null)
        {
            source.PlayOneShot(shootSFX);
        }
    }

    public void PlayReload()
    {
        if (source != null && reloadSFX != null)
        {
            source.PlayOneShot(reloadSFX);
        }
    }

    public void PlayHolster()
    {
        if (source != null && holsterSFX != null)
        {
            source.PlayOneShot(holsterSFX);
        }
    }

    public void PlayUnholster()
    {
        if (source != null && unholsterSFX != null)
        {
            source.PlayOneShot(unholsterSFX);
        }
    }
}
