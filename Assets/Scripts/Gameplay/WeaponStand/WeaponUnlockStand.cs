using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUnlockStand : BaseInteractable
{
    [SerializeField] private WeaponType weaponType;
    [Seperator]
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private Collider coll;

    private WeaponHolder weaponHold;

    private GameObject weaponObj;

    public override bool Interact()
    {
        if (base.Interact())
        {
            WeaponManager.Instance.AddWeapon(weaponObj);
            coll.enabled = false;

            return true;
        }

        return false;
    } 

    public override void Start()
    {
        base.Start();

        unlockCost = weaponHold.Cost;

        if (weaponType == WeaponType.NULLED)
        {
            coll.enabled = false;
            return;
        }

        weaponHold = WeaponPoolManager.Instance.GetGunGO(weaponType);
        weaponObj = Instantiate(weaponHold.WeaponPrefab, weaponPoint);
        costDisplay.text = weaponType.ToString() + "\n$" + weaponHold.Cost.ToString();

        UpdateTextColor();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void UpdateTextColor()
    {
        if (PointsManager.Instance.CurrPts >= weaponHold.Cost)
        {
            costDisplay.color = Color.green;
        }
        else
        {
            costDisplay.color = Color.red;
        }
    }
}
