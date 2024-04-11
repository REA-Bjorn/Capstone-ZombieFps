using UnityEngine;

public class MaxAmmoPickup : BasePickup
{
    public override void Pickup()
    {
        Debug.Log("Max Ammo Pickup Up");
        // WeaponManager.Instance.RefillAllWeapon();
    }
}
