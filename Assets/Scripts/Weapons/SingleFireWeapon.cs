using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireWeapon : BaseWeapon
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
        if (base.Shoot() && ammo.IsValid)
        {
            // Plays the muzzle flash animation
            muzzleFlash.Play();

            // Runs the timer for fire rate
            fireRateTimer.StartTimer();

            // Plays gun shot audio
            audioScript?.PlayShoot();

            // Use an ammo because we can shoot it
            ammo.Decrease(1f);
            UIManager.Instance.UpdateWeaponsUI();

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
