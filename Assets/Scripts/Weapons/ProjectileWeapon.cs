using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    [Seperator]
    [SerializeField] protected GameObject ammoPrefabToSpawn;
    [SerializeField] protected Transform spawnPoint;
    [Header("Can be null:")]
    [SerializeField] protected MeshFilter currentModel;
    [SerializeField] protected Mesh unarmedModel;
    [SerializeField] protected Mesh armedModel;

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

    public override void Reload(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        base.Reload(context);

        DetermineMeshStats();
    }

    public override bool Shoot()
    {
        if (base.Shoot() && ammo.IsValid)
        {
            WeaponFX();
            DetermineMeshStats();
            
            GameObject spawnedProjectile = Instantiate(ammoPrefabToSpawn, spawnPoint.position, spawnPoint.rotation);
            spawnedProjectile.GetComponent<Projectile>().Startup(WeaponManager.Instance.CurrentAttack);
        }

        return true;
    }

    private void DetermineMeshStats()
    {
        if (ammo.IsValid)
        {
            currentModel.mesh = armedModel;
        }
        else
        {
            currentModel.mesh = unarmedModel;
        }
    }
}
