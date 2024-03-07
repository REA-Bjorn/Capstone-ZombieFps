using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : WeaponBase
{
    public override void Shoot()
    {
        Debug.Log("Shoot");
        if (CanUse)
        {
            timer.enabled = true;
            timer.StartTimer(cooldown);
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, range))
            {
                IDamage damage = hit.collider.GetComponent<IDamage>();
                if (damage != null)
                {
                    damage.TakeDamage(attack.CurrentValue);
                }
                else
                {
                    Debug.Log("Damage is Null");
                }
            }
        }
    }
}
