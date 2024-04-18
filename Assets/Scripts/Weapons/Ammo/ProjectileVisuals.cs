using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTimer))]
public class ProjectileVisuals : MonoBehaviour
{
    [SerializeField] private CustomTimer deathTimer;

    private void Start()
    {
        deathTimer.OnEnd += Died;
        deathTimer.StartTimer();
    }

    private void Died()
    {
        Destroy(gameObject);
    }
}
