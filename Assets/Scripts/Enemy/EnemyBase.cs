using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamage
{
    [SerializeField] HealthPool health;
    [SerializeField] AttackPool attack;
    [SerializeField] SpeedPool speed;
    [SerializeField] EnemyMovement move;
    [SerializeField] SkinnedMeshRenderer mesh;
    [SerializeField] Material baseMaterial;
    [SerializeField] Material hitMaterial;
    [SerializeField] int PointVal;

    [SerializeField] private Animator animator;

    public SpeedPool Spd => speed;
    public AttackPool Atk => attack;

    // Start is called before the first frame update
    void Start()
    {
        MaxStats();
        mesh.material = baseMaterial;

        if (mesh == null)
        {
            mesh = GetComponent<SkinnedMeshRenderer>();
        }

        health.OnDepleted += Health_OnDepleted;
    }
    private void Health_OnDepleted()
    {
        PickupManager.Instance.DropPickup(transform);
        PointsManager.instance.AddPoints(PointVal);
        UIManager.Instance.UpdateScore();
        WaveManager.Instance.EnemyKilled(gameObject);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("I am moving!!");
        move.Movement();
    }

    public void TakeDamage(float damage)
    {
        health.UseResource(damage);
    }

    public void MaxStats()
    {
        attack.SetMax();
        health.SetMax();
        speed.SetMax();
    }

    /// <summary>
    /// Calls Max Stats and Spawn Animation player.
    /// </summary>
    public void SpawnMe()
    {
        MaxStats();
        PlaySpawnAnimation();
    }

    public void ForceKill()
    {
        TakeDamage(health.CurrentValue);
    }

    private void PlaySpawnAnimation()
    {
        //animator.SetTrigger("Spawn");
    }

    public void TakeMaxDamage()
    {
        ForceKill();
    }

    //private IEnumerator FlashDamage()
    //{
    //    mesh.material = hitMaterial;
    //    yield return new WaitForSeconds(0.25f);
    //    mesh.material = baseMaterial;
    //}
}
