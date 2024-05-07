using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blockade : BaseInteractable
{
    public event Action OnSuccessfulInteract;
    [SerializeField] private List<Blockade> linkedBlockades;

    private bool boughtStatus = false;
    public bool Bought => boughtStatus;

    public override bool Interact()
    {
        if (base.Interact())
        {
            boughtStatus = true;
            OnSuccessfulInteract?.Invoke();

            // Loop through all linked blockades and turn it off
            foreach (var blockade in linkedBlockades)
            {
                blockade.gameObject.SetActive(false);
            }

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
