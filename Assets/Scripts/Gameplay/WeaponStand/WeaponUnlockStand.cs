using System;
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
        // Specifically for the Weapon Stand
        if (!boughtWeapon)
        {
            // Regular Interact() Functionality here, 
            if (base.Interact())
            {
                // Need weapon and has paid
                WeaponManager.Instance.AddWeapon(weaponObj);
                ammoObj.SetActive(true);
                boughtWeapon = true;

                // Update new unlock cost
                unlockCost = weaponHold.Cost / 2f;
                costDisplay.text = weaponType.ToString() + "Ammo \n$" + unlockCost.ToString();
                return true;
            }
            else
            {
                UIManager.Instance.PlayerUIScript.FlashPointsUI();
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
                    UIManager.Instance.PlayerUIScript.FlashPointsUI();
                }
            }
            else
            {
                Debug.Log("Player doesn't have weapons!");
            }
        }

        /* 
         * One of the following happened:
         *  - We have not bought the weapon off the stand
         *      - We do not have enough points to do so.
         *  - We have the weapon
         *      - We do not have enough points to buy ammo
         */
        return false; // we did not pass the vibe check to buy gun/ammo
    }

    public override void Start()
    {
        base.Start();


        coll.enabled = true;

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
        // Set parent stand
        weaponObj.GetComponent<BaseWeapon>().SetParentStandObj(weaponPoint.gameObject, this);

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
        if (PointsManager.Instance.CurrPts >= unlockCost)
        {
            costDisplay.color = Color.green;
        }
        else
        {
            costDisplay.color = Color.red;
        }
    }

    public void UnbuyWeaponFunctionality()
    {
        boughtWeapon = false;
        ammoObj.SetActive(false);
        unlockCost = weaponHold.Cost;
        costDisplay.text = weaponType.ToString() + "\n$" + unlockCost.ToString();
    }
}
