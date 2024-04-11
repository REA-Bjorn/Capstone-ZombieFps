using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour, IPickupable
{
    public event Action OnPickup;

    [SerializeField] private CustomTimer despawnTimer;

    private void Start()
    {
        despawnTimer.OnEnd += () => { Destroy(gameObject); };

        despawnTimer.StartTimer();
    }

    public virtual void Pickup()
    {
        //Debug.Log("Base Pickup Function Called");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickup?.Invoke();
            Pickup();
            Destroy(gameObject);
        }
    }
}
