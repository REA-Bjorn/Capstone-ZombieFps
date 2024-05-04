using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunScript : BaseWeapon
{
    [Seperator]

    [SerializeField] int NumberOfPellets;

    [SerializeField] float MaxSpread;

    [SerializeField] private GameObject sphereMesh;
    [SerializeField] private GameObject capsMesh;

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

            // Create a raycast hit to store hit information
            RaycastHit hit;

            // Loop through all pellets
            for (int i = 0; i < NumberOfPellets; i++)
            {
                Vector3 direction = Camera.main.transform.forward;
                Vector3 spread = Camera.main.transform.up * Random.Range(-MaxSpread, MaxSpread);
                
                spread += Camera.main.transform.right * Random.Range(-MaxSpread, MaxSpread);
                direction += spread.normalized * Random.Range(0f, 0.2f);

                //Debug.DrawRay(transform.position, direction, Color.blue, 100000);

                if (Physics.Raycast(transform.position, direction, out hit, ShootDist))
                {
                    //Debug.DrawLine(transform.position, hit.point, Color.red, 100000);

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
