using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamage
{
    [SerializeField] HealthPool health;
    [SerializeField] AttackPool attack;
    [SerializeField] SpeedPool speed;
    [Seperator]
    [SerializeField] private EnemyVisual visualScript;
    [SerializeField] EnemyMovement move;
    [SerializeField] int PointVal;

    [SerializeField] private Animator animator;
    private bool distracted;

    public SpeedPool Spd => speed;
    public AttackPool Atk => attack;

    // Start is called before the first frame update
    void Start()
    {
        MaxStats();
        visualScript.SetRandomMaterial();
        health.OnDepleted += Health_OnDepleted;
    }

    private void Health_OnDepleted()
    {
        PickupManager.Instance.DropPickup(transform);
        PointsManager.Instance.AddPoints(PointVal);
        WaveManager.Instance.EnemyKilled(gameObject);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (distracted)
        {
            move.DistractedMovement();
        }
        else
        {
            move.Movement();
        }
    }

    public void TakeDamage(float damage)
    {
        health.Decrease(damage);

        if (health.IsValid)
        {
            // Update enemy speed based off of their health's percent and their min/max speed
            float newSpeed = Mathf.Clamp(health.Percent * speed.Max, speed.Min, speed.Max);
            move.UpdateMoveSpeed(newSpeed);
        }
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
        //PlaySpawnAnimation();
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

    public void ToggleDistracted()
    {
        distracted = !distracted;
    }
}
