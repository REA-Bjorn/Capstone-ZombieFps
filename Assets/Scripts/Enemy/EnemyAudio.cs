using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] ambientGroanSFX;
    [SerializeField] private AudioClip[] hitSFX;
    [Seperator]
    [SerializeField] private CustomTimer groanTimer;
    [SerializeField] private float minWaitTime = 2.0f;
    [SerializeField] private float maxWaitTime = 5.0f;

    public void TurnOn()
    {
        groanTimer.OnEnd += PlayRandomGroan;
        groanTimer.StartTimer(Random.Range(minWaitTime, maxWaitTime));
    }

    public void TurnOff()
    {
        groanTimer.OnEnd -= PlayRandomGroan;
    }

    public void PlayRandomGroan()
    {
        if (source != null && ambientGroanSFX.Length > 0)
        {
            source.PlayOneShot(ambientGroanSFX[Random.Range(0, ambientGroanSFX.Length)]);
            groanTimer.StartTimer(Random.Range(minWaitTime, maxWaitTime));
        }
    }

    public void PlayRandomHit()
    {
        if (source != null && hitSFX.Length > 0)
        {
            source.PlayOneShot(hitSFX[Random.Range(0, hitSFX.Length)]);
        }
    }

}
