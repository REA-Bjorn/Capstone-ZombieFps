using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blockade : BaseInteractable
{
    public event Action OnSuccessfulInteract;

    private bool boughtStatus = false;
    public bool Bought => boughtStatus;

    public override bool Interact()
    {
        if (base.Interact())
        {
            boughtStatus = true;
            OnSuccessfulInteract?.Invoke();
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }
    public override void Start()
    {
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void UpdateTextColor()
    {
        base.UpdateTextColor();
    }
}
