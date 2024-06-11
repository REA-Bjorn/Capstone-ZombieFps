using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunScript : BaseWeapon
{
    [Seperator]
    //Can set the number of pellets
    [SerializeField] int NumberOfPellets;
    //Can set the spread amount
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
        if (base.Shoot())
        {

            // Create a raycast hit to store hit information
            RaycastHit hit;

            // Loop through all pellets
            for (int i = 0; i < NumberOfPellets; i++)
            {
                //Create two vectors direction and spread for the shot being created
                Vector3 direction = Camera.main.transform.forward;
                Vector3 spread = Camera.main.transform.up * Random.Range(-MaxSpread, MaxSpread);
                //Updating the vectors to a new value
                spread += Camera.main.transform.right * Random.Range(-MaxSpread, MaxSpread);
                direction += spread.normalized * Random.Range(0f, 0.2f);

                if (Physics.Raycast(transform.position, direction, out hit, ShootDist))
                {
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
        }
        return true;
    }

}
