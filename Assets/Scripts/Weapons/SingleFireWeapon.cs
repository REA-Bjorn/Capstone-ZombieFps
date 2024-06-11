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
        if (base.Shoot())
        {
            // Create Raycast Hit data
            RaycastHit hit;
            // Shoot a ray from the screen with our shoot dist
            // COD World At War also does it from the camera (tested this in-game)
            //First Shot
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, ShootDist))
            {
                // Get an IDamage component from the hit object
                IDamage damage = hit.collider.GetComponent<IDamage>();
                if (damage != null)
                {
                    UIManager.Instance.PlayerUIScript.FlashHitMarker();
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
