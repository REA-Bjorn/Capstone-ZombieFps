using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour, IPickupable
{
    public event Action OnPickup;

    [SerializeField] private CustomTimer despawnTimer;
    [SerializeField] private AudioClip pickupSFX;
    [SerializeField] private AudioClip voiceLine;


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
            PlayerBase.instance.ExtraSFX(pickupSFX);
            PlayerBase.instance.ExtraSFX(voiceLine);
            Destroy(gameObject);
        }
    }
}
