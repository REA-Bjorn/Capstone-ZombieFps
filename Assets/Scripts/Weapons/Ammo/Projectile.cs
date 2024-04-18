using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTimer), typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private CustomTimer deathTimer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject projectileDeathEffects;
    [Seperator]
    [SerializeField] private float aoeRange;
    [SerializeField] private float speed;
    [SerializeField] private bool canHurtPlayer;

    // Here to avoid creating garbage memory
    private float damage = 0;
    private float distance = 0;
    private float applied = 0;

    public void Startup(float dmgAmt)
    {
        // sub the timer cause we are about to be launched
        deathTimer.OnEnd += ProjectileDied;
        deathTimer.OnTick += ProjectileTick;
        deathTimer.StartTimer();

        damage = dmgAmt;
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void ProjectileTick()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime));
    }

    private void ProjectileDied()
    {
        deathTimer.StopTimer();

        Instantiate(projectileDeathEffects, transform.position, transform.rotation);

        // unsub the timer cause the projectile is about to be destroyed
        deathTimer.OnEnd -= ProjectileDied;
        Collider[] allHits = Physics.OverlapSphere(transform.position, aoeRange);
        foreach (Collider hit in allHits)
        {
            // If we cannot damage the player and we hit the player, continue the loop
            if (!canHurtPlayer && hit.CompareTag("Player"))
            {
                continue;
            }
            else if (canHurtPlayer && hit.CompareTag("Player"))
            {
                PlayerBase.instance.ShakeCam(1.5f, 0.5f);
            }

            IDamage dmg = hit.GetComponent<IDamage>();

            if (dmg != null)
            {
                if (WeaponManager.Instance.InstaKill)
                {
                    dmg.TakeMaxDamage();
                }
                else
                {
                    distance = Vector3.Distance(hit.transform.position, transform.position);
                    applied = Mathf.Clamp((distance / aoeRange), 0.5f, aoeRange) * damage;
                    dmg.TakeDamage(applied);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the collider hit anything!
        ProjectileDied();
    }
}
