using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerBase : MonoBehaviour, IDamage
{
    public static PlayerBase instance;

    [SerializeField] HealthPool health;
    [SerializeField] SpeedPool speed;
    [Seperator]
    [SerializeField] PlayerMovement move;
    [SerializeField] PlayerCamera cam;
    [SerializeField] CustomTimer respawnTimer;
    [SerializeField] CustomTimer staminaTimer;

    // Properties
    public SpeedPool Spd => speed;
    public HealthPool Health => health;
    public float ShootDist => WeaponManager.Instance.ShootDist;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed.SetMax();
        health.SetMax();
        health.OnDepleted += HealthDepleted;

        respawnTimer.OnStart += RespawnTimer_OnStart;
        respawnTimer.OnEnd += RespawnTimer_OnEnd;
        
    }

    private void RespawnTimer_OnEnd()
    {
        speed.SetMax();
        health.SetMax();
    }

    private void RespawnTimer_OnStart()
    {
        GameManager.Instance.PlayerReviving();
    }

    private void HealthDepleted()
    {
        if (PerkManager.Instance.SecondaryLife)
        {
            respawnTimer.StartTimer();
        }
        else
        {
            LockPlayer();
            UIManager.Instance.DeathMenu();
        }
    }

    public void LockPlayer()
    {
        move.enabled = false;
        cam.enabled = false;

        WeaponManager.Instance.DisableWeapon();
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        health.OnDepleted -= HealthDepleted;
    }

    void Update()
    {
        move.Movement();

        if (cam.enabled)
        {
            cam.Look();
        }
    }

    public void TakeDamage(float damage)
    {
        health.Decrease(damage);
    }

    public void TakeMaxDamage()
    {
        // nothing here, player should never take max damage
    }

    public void ShakeCam(float camShakeAmount, float camShakeDuration)
    {
        cam.TurnOnCamShake(camShakeAmount, camShakeDuration);
    }
}