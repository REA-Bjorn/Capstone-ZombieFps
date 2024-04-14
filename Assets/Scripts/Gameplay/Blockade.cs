using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blockade : BaseInteractable
{
    public override bool Interact()
    {
        if (base.Interact())
        {
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
