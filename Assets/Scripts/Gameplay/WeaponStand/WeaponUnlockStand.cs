using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlockStand : BaseInteractable
{
    [Seperator]
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private GameObject ammoObj;
    [SerializeField] private Collider coll;

    private WeaponHolder weaponHold;
    private GameObject weaponObj;

    private bool boughtWeapon;

    public override bool Interact()
    {
        if (!boughtWeapon)
        {
            if (base.Interact())
            {
                // Need weapon and has paid
                WeaponManager.Instance.AddWeapon(weaponObj);
                ammoObj.SetActive(true);
                boughtWeapon = true;

                // Update new unlock cost
                unlockCost = weaponHold.Cost / 2f;

                return true;
            }
            else
            {
                // not enough points, show on UI manager as read points
            }
        }
        else
        {
            // check if player has the current weapon
            bool hasWeaponAndNeedsAmmo = WeaponManager.Instance.HasWeapon(weaponType);

            if (hasWeaponAndNeedsAmmo)
            {
                // We have the weapon, but do we have the currency
                if (base.Interact())
                {
                    WeaponManager.Instance.BoughtAmmo(weaponType);
                    UIManager.Instance.UpdateWeaponsUI();
                    return true;
                }
                else
                {
                    // not enough points, show on UI manager as read points
                    return false;
                }
            }

            // we don't have the weapon
            return false;
        }

        return false;
    }

    public override void Start()
    {
        base.Start();

        // Make sure its not null.
        // If it is turn off collider and don't interact with this obj
        if (weaponType == WeaponType.NULLED)
        {
            coll.enabled = false;
            return;
        }

        boughtWeapon = false;

        // Turn off Ammo Obj just in case it was on
        ammoObj.SetActive(false);

        // Get Current Weapon Data
        weaponHold = WeaponPoolManager.Instance.GetGunGO(weaponType);
        weaponObj = Instantiate(weaponHold.WeaponPrefab, weaponPoint);

        // Set Unlock cost to weapons cost and update text display
        unlockCost = weaponHold.Cost;
        costDisplay.text = weaponType.ToString() + "\n$" + unlockCost.ToString();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PointsManager.Instance.AddPoints(50f);
        }
    }
}
