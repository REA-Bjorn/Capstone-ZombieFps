using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private List<AudioClip> hitSFX;

    public void PlayHitSFX()
    {
        if (hitSFX.Count > 0)
            playerAudioSource.PlayOneShot(hitSFX[Random.Range(0, hitSFX.Count)]);
    }
}
