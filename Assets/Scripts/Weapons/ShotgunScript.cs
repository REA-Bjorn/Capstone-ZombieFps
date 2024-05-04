using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : BaseWeapon
{
    [Seperator]

    [SerializeField] int NumberOfPellets;

    [SerializeField] float MaxSpread;
    
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
            WeaponFX();


            for (int i = 0; i < NumberOfPellets; i++)
            {
                Ray CameraRay = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

                Physics.Raycast(CameraRay, ShootDist);

                var a = Quaternion.LookRotation(CameraRay.direction);

                a = Quaternion.Euler(a.eulerAngles.x + UnityEngine.Random.Range(0, MaxSpread), a.eulerAngles.y + UnityEngine.Random.Range(0, MaxSpread), a.eulerAngles.z);

                RaycastHit hit;

                Debug.Log(a.eulerAngles);

                if (Physics.Raycast(Camera.main.transform.forward, a.eulerAngles, out hit, ShootDist))
                {
                    Debug.DrawLine(Camera.main.transform.forward, a.eulerAngles, Color.red);
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
        }
        return true;
    }

}
