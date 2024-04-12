using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum WeaponType
{
    NULLED,
    Pistol,
    AssaultRifle,
    MachineGun,
    LightMachineGun,
    SubMachineGun,
    Sniper,
    LightningStaff,
    RocketLauncher,
    Shotgun
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
        retGun.WeaponPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
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
