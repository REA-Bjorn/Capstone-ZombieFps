using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void PlayShoot()
    {
        if (source != null)
        {
            source.Play();
        }
    }
}
