using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkStation : BaseInteractable
{
    // how to override, first change MonoBehavior to BaseInteractable
    // then type 'override' and the rest should pop up

    // override the Interact function
    public override bool Interact()
    {
        return base.Interact();
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
