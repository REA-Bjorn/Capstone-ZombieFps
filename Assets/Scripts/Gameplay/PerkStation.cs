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

    // override the Start Function
    public override void Start()
    {
        base.Start();
    }

    // override the OnDestroy function (leave the base in there)
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

}
