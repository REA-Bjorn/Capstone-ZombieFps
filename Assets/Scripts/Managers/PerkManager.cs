using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public static PerkManager Instance;

    [SerializeField] private bool doubleHealth;
    [SerializeField] private bool fasterReload;
    [SerializeField] private bool fasterShoot;
    [SerializeField] private bool fasterSprint;
    [SerializeField] private bool secondLife;

    public bool DoubleHealth => doubleHealth;
    public bool FasterReload => fasterReload;
    public bool FasterSprint => fasterSprint;
    public bool FasterShoot => fasterShoot;
    public bool SecondaryLife => secondLife;

    public Texture FireRateSprite;
    public Texture HealthSprite;
    public Texture ReloadSpeedSprite;
    public Texture SecondLifeSprite;
    public Texture MoveSpeedSprite;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetAllPerks();
    }

    public void UnlockPerk(PerkType _type)
    {
        switch (_type)
        {
            case PerkType.FireRate:
                fasterShoot = true;
                break;
            case PerkType.Health:
                doubleHealth = true;
                break;
            case PerkType.ReloadSpeed:
                fasterReload = true;
                break;
            case PerkType.SecondLife:
                secondLife = true;
                break;
            case PerkType.MoveSpeed:
                fasterSprint = true;
                break;
            case PerkType.NULLPERK:
                break;
            default:
                break;
        }
        if (_type != PerkType.NULLPERK)
        {
            UIManager.Instance.PerkUIScript.UnlockedNewPerk(_type);
        }
    }

    public void ResetAllPerks()
    {
        // reset double health
        doubleHealth = false;
        fasterReload = false;   
        fasterShoot = false;
        fasterSprint = false;
        secondLife = false;

        UIManager.Instance.PerkUIScript.ResetPerks();
    }
}
