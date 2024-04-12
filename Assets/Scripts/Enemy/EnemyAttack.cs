using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof (CustomTimer))]
public class EnemyAttack : MonoBehaviour 
{
    [SerializeField] BoxCollider box;

    [SerializeField] EnemyBase enemy;

    [SerializeField] CustomTimer timer;

    private float storedTime;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamage dmg = other.GetComponent<IDamage>();
            if (dmg != null)
            {
                dmg.TakeDamage(enemy.Atk.CurrentValue);
                timer.StartTimer(storedTime);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        storedTime = timer.DurationTime;
        timer.OnStart += Timer_OnStart;
        timer.OnEnd += Timer_OnEnd;
    }

    private void OnDisable()
    {
        timer.OnStart -= Timer_OnStart;
        timer.OnEnd -= Timer_OnEnd;
    }

    private void Timer_OnEnd()
    {
        box.enabled = true;
    }

    private void Timer_OnStart()
    {
        box.enabled = false;
    }
}
