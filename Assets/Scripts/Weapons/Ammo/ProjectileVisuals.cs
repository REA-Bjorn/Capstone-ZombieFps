using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(CustomTimer))]
public class ProjectileVisuals : MonoBehaviour
{
    [SerializeField] private CustomTimer deathTimer;
    [SerializeField] private Light _light;
    [SerializeField] private ParticleSystem particles;
    [Seperator]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private float lightRangeStart = 0;
    private float lightRangeEnd = 0;

    public void Startup()
    {
        lightRangeStart = _light.range;
        lightRangeEnd = 0;

        particles.Play();

        source.PlayOneShot(clip);
        deathTimer.OnTick += Ticked;
        deathTimer.StartTimer();
    }

    private void Ticked()
    {
        _light.range = Mathf.Lerp(lightRangeEnd, lightRangeStart, deathTimer.ReversePercentage);
    }
}
