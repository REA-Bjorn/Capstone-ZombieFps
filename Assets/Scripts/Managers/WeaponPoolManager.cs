using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum WeaponType
{
    NULLED,
    AssaultRifle,
    Pistol,
    ConcealCarry,
    SMG,
    SniperRifle,
    LightningStaff,
    RocketLauncher,
    Shotgun,
    Granade
}

[Serializable]
public struct WeaponHolder
{
    public int Cost;
    public WeaponType Weapon;
    public GameObject WeaponPrefab;
}

public class WeaponPoolManager : MonoBehaviour
{
    public static WeaponPoolManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private List<WeaponHolder> allWeapons;

    public WeaponHolder GetGunGO(WeaponType type)
    {
        WeaponHolder retGun;
        retGun.Weapon = WeaponType.NULLED;
        retGun.WeaponPrefab = null;
        retGun.Cost = 0;

        foreach (WeaponHolder gun in allWeapons)
        {
            if (gun.Weapon == type)
            {
                retGun = gun;

            }
        }

        return retGun;
    }
}
