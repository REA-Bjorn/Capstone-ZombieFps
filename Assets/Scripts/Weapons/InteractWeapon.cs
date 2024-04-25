using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWeapon : BaseWeapon
{
    public override void Start()
    {
        base.Start();
    }

    public override void WeaponOn()
    {
        base.WeaponOn();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override bool Shoot()
    {
        if (base.Shoot())
        {
            WeaponFX();

            // Create Raycast Hit data
            RaycastHit hit;
            // Shoot a ray from the screen with our shoot dist
            // COD World At War also does it from the camera (tested this in-game)
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, ShootDist))
            {
                // Get an IDamage component from the hit object
                IDamage damage = hit.collider.GetComponent<IDamage>();
                if (damage != null)
                {
                    damage.TakeDamage(attack.CurrentValue);
                }
            }
        }

        return true;
    }
}
