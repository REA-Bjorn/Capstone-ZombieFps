using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkUI : MonoBehaviour
{
    [SerializeField] private List<PerkSlot> perks;

    public void UnlockedNewPerk(PerkType type)
    {
        foreach (PerkSlot slot in perks)
        {
            if (!slot.Filled)
            {
                slot.SetPerkUI(type);
                return;
            }
        }
    }

    public void ResetPerks()
    {
        foreach (PerkSlot slot in perks)
        {
            slot.SetPerkUI(PerkType.NULLPERK);
        }
    }
}
