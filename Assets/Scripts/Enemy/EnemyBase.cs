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
    [SerializeField] private EnemyAudio audioScript;
    [SerializeField] EnemyMovement move;
    [SerializeField] int deathPointWorth = 50;
    [SerializeField] int hitPointVal = 15;

    [SerializeField] private Animator animator;
    private bool distracted;

    public SpeedPool Spd => speed;
    public AttackPool Atk => attack;

    void Start()
    {
        MaxStats();
        // Force update on start of enemy because waves starts at 0
        // therefore starting hp = 0 bad...
        health.UpdateMax(1.41425f);
        health.SetMax();

        // Update extra visuals
        visualScript.UpdateEnemyEyes(health.Percent * 10);
        visualScript.SetRandomMaterial();
        health.OnDepleted += Health_OnDepleted;
    }

    private void Health_OnDepleted()
    {
        PickupManager.Instance.DropPickup(transform);
        WaveManager.Instance.EnemyKilled(gameObject);
        gameObject.SetActive(false);
    }

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

    public void TakeDamage(float damage, bool forceKilled = false)
    {
        health.Decrease(damage);
        visualScript.UpdateEnemyEyes(health.Percent * 10);

        if (health.IsValid)
        {
            PointsManager.Instance.AddPoints(hitPointVal);
            audioScript.PlayRandomHit();
            // Update enemy speed based off of their health's percent and their min/max speed
            float newSpeed = Mathf.Clamp(health.Percent * speed.Max, speed.Min, speed.Max);
            move.UpdateMoveSpeed(newSpeed);
        }

        // If we are not forcing the kill on the enemy and they did "die"
        if (!forceKilled && !health.IsValid)
        {
            PointsManager.Instance.AddPoints(deathPointWorth);
        }
    }

    public void MaxStats()
    {
        attack.SetMax();
        speed.SetMax();

        health.UpdateMax(WaveManager.Instance.CurrWaveNumInt * 1.4142f);
        health.SetMax();

        visualScript.UpdateEnemyEyes(health.Percent * 10);
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
        TakeDamage(health.CurrentValue, true);
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
