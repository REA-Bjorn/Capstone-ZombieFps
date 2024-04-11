using UnityEngine;

public class InstaKillPickup : BasePickup
{
    public override void Pickup()
    {
        Debug.Log("Insta Kill Pickup Up");
        // WeaponManager.Instance.EnableInstaKill();
    }
}
