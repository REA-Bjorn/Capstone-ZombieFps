using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PerkStation : BaseInteractable
{
    [SerializeField] PerkType type;
    [SerializeField] private Collider coll;
    [SerializeField] private GameObject perkPoint;

    public override bool Interact()
    {
        if (type == PerkType.SecondLife && !GameManager.Instance.CanBuyRevive)
            return false; 
        
        if (base.Interact())
        {
            PerkManager.Instance.UnlockPerk(type);
            coll.enabled = false;
            perkPoint.SetActive(false);
            return true;
        }
        else
            return false;
    }

    public override void Start()
    {
        base.Start();

        PerkManager.Instance.ResetStand += ResetStand;
    }

    private void ResetStand()
    {
        if (type == PerkType.SecondLife && !GameManager.Instance.CanBuyRevive)
            return;
        
        coll.enabled = true;
        perkPoint.SetActive(true);
    }

    public override void OnDestroy()
    {
        PerkManager.Instance.ResetStand -= ResetStand;
        base.OnDestroy();
    }

}
