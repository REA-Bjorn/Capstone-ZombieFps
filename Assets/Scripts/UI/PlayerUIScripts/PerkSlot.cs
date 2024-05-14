using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum PerkType
{
    Firing,
    Health,
    Reload,
    Revive,
    Speed,
    NULLPERK
}

public class PerkSlot : MonoBehaviour
{
    [SerializeField] private PerkType perkType;
    [SerializeField] private RawImage displayPerk;

    private bool filled = false;
    public bool Filled => filled;

    public void SetPerkUI(PerkType _perk)
    {
        switch (_perk)
        {
            case PerkType.Firing:
                displayPerk.color = Color.white;
                filled = true;
                displayPerk.texture = PerkManager.Instance.FireRateSprite;
                break;
            case PerkType.Health:
                displayPerk.color = Color.white;
                filled = true;
                displayPerk.texture = PerkManager.Instance.HealthSprite;
                break;
            case PerkType.Reload:
                displayPerk.color = Color.white;
                filled = true;
                displayPerk.texture = PerkManager.Instance.ReloadSpeedSprite;
                break;
            case PerkType.Revive:
                displayPerk.color = Color.white;
                filled = true;
                displayPerk.texture = PerkManager.Instance.SecondLifeSprite;
                break;
            case PerkType.Speed:
                displayPerk.color = Color.white;
                filled = true;
                displayPerk.texture = PerkManager.Instance.MoveSpeedSprite;
                break;
            case PerkType.NULLPERK:
                displayPerk.color = Color.clear;
                filled = false;
                displayPerk.texture = null;
                break;
            default:
                displayPerk.color = Color.clear;
                filled = false;
                displayPerk.texture = null;
                break;
        }
    }
}
