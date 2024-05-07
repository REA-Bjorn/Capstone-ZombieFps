using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [Seperator]
    [SerializeField] private List<AudioClip> shootSFX;
    [SerializeField] private List<AudioClip> reloadSFX;
    [SerializeField] private List<AudioClip> holsterSFX;
    [SerializeField] private List<AudioClip> unholsterSFX;

    public void PlayShoot()
    {
        if (source != null && shootSFX != null && source.isActiveAndEnabled)
        {
            if (shootSFX.Count > 0)
                source.PlayOneShot(shootSFX[Random.Range(0,shootSFX.Count)]);
        }
    }

    public void PlayReload()
    {
        if (source != null && reloadSFX != null && source.isActiveAndEnabled)
        {
            if(reloadSFX.Count > 0)
            source.PlayOneShot(reloadSFX[Random.Range(0, reloadSFX.Count)]);
        }
    }

    public void PlayHolster()
    {
        if (source != null && holsterSFX != null && source.isActiveAndEnabled)
        {
            if(holsterSFX.Count > 0)
                source.PlayOneShot(holsterSFX[Random.Range(0, holsterSFX.Count)]);
        }
    }

    public void PlayUnholster()
    {
        if (source != null && unholsterSFX != null && source.isActiveAndEnabled)
        {
            if(unholsterSFX.Count > 0)
                source.PlayOneShot(unholsterSFX[Random.Range(0, unholsterSFX.Count)]);
        }
    }
}
