using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] ambientGroanSFX;
    [SerializeField] private AudioClip[] hitSFX;
    [Seperator]
    [SerializeField] private float minWaitTime = 2.0f;
    [SerializeField] private float maxWaitTime = 5.0f;

    private void Start()
    {
        StartCoroutine(PlayRandomGroan());
    }

    public IEnumerator PlayRandomGroan()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        
        if (source != null && ambientGroanSFX.Length > 0)
        {
            float dbg = Random.Range(minWaitTime, maxWaitTime);
            source.PlayOneShot(ambientGroanSFX[Random.Range(0, ambientGroanSFX.Length)]);
        }

        StopAllCoroutines();
        StartCoroutine(PlayRandomGroan());
    }

    public void PlayRandomHit()
    {
        if (source != null && hitSFX.Length > 0)
        {
            source.PlayOneShot(hitSFX[Random.Range(0, hitSFX.Length)]);
        }
    }

}
