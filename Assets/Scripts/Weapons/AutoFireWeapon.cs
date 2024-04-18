using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AutoFireWeapon : BaseWeapon
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

    private void Update()
    {
        if (CanUse && Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    public override bool Shoot()
    {
        if (base.Shoot() && ammo.IsValid)
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
                    // if we hit something and that hit has the IDamage component
                    // call its take damage function
                    if (WeaponManager.Instance.InstaKill)
                    {
                        damage.TakeMaxDamage();
                    }
                    else
                    {
                        damage.TakeDamage(attack.CurrentValue);
                    }
                }
            }
        }

        return true;
    }
}
