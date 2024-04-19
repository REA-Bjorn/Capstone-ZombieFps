using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    [Seperator]
    [SerializeField] protected GameObject ammoPrefabToSpawn;
    [SerializeField] protected Transform spawnPoint;

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
            GameObject spawnedProjectile = Instantiate(ammoPrefabToSpawn, spawnPoint.position, spawnPoint.rotation);
            spawnedProjectile.GetComponent<Projectile>().Startup(WeaponManager.Instance.CurrentAttack);
        }

        return true;
    }
}
