using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTimer))]
public class ProjectileVisuals : MonoBehaviour
{
    [SerializeField] private CustomTimer deathTimer;
    [SerializeField] private Light _light;
    [Seperator]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private float lightRangeStart = 0;
    private float lightRangeEnd = 0;

    private void Start()
    {
        lightRangeStart = _light.range;
        lightRangeEnd = 0;

        deathTimer.OnEnd += Died;
        deathTimer.OnTick += Ticked;
        deathTimer.StartTimer();
    }

    private void Ticked()
    {
        _light.range = Mathf.Lerp(lightRangeStart, lightRangeEnd, deathTimer.ReversePercentage);
    }

    private void Died()
    {
        source.PlayOneShot(clip);
        Destroy(gameObject);
    }
}
