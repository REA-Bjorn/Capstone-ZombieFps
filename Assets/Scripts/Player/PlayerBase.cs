using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerBase : MonoBehaviour, IDamage
{
    public static PlayerBase instance;

    [SerializeField] private HealthPool health;
    [SerializeField] private SpeedPool speed;
    [SerializeField] private StaminaPool stamina;
    [Seperator]
    [SerializeField] private PlayerMovement move;
    [SerializeField] private PlayerCamera cam;
    [SerializeField] private CustomTimer respawnTimer;
    [SerializeField] private AudioSource extraSFXSource;
    [Seperator]
    [SerializeField] private CustomTimer passiveHealthRegenTimer;
    [SerializeField] private CustomTimer delayPassiveHealthRegenTimer;

    // Properties
    public SpeedPool Spd => speed;
    public HealthPool Health => health;
    public StaminaPool Stam => stamina;
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
        stamina.SetMax();
        stamina.OnChanged += UIManager.Instance.PlayerUIScript.UpdateStaminaBar;
        health.OnDepleted += HealthDepleted;

        respawnTimer.OnStart += RespawnTimer_OnStart;
        respawnTimer.OnEnd += RespawnTimer_OnEnd;

        delayPassiveHealthRegenTimer.OnEnd += () =>
        {
            passiveHealthRegenTimer.StartTimer();
        };

        passiveHealthRegenTimer.OnTick += () =>
        {
            health.Increase(Time.deltaTime * 0.1f); // time.delta time will be a lot so 0.1 times that
            
            if (health.IsMaxed)
                passiveHealthRegenTimer.StopTimer();
        };
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
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        health.OnDepleted -= HealthDepleted;
    }

    private void OnDestroy()
    {
        stamina.OnChanged -= UIManager.Instance.PlayerUIScript.UpdateStaminaBar;
        health.OnDepleted -= HealthDepleted;

        respawnTimer.OnStart -= RespawnTimer_OnStart;
        respawnTimer.OnEnd -= RespawnTimer_OnEnd;

        delayPassiveHealthRegenTimer.OnEnd -= () =>
        {
            passiveHealthRegenTimer.StartTimer();
        };

        passiveHealthRegenTimer.OnTick -= () =>
        {
            health.Increase(Time.deltaTime * 0.1f);

            if (health.IsMaxed)
                passiveHealthRegenTimer.StopTimer();
        };
    }

    void Update()
    {
        move.Movement();

        if (cam.enabled)
        {
            cam.Look();
        }
    }

    public void TakeDamage(float damage, bool forceKilled = false)
    {
        // Show visual feedback
        UIManager.Instance.PlayerHitScript.Active();

        // Timer code for regen resetting
        if (passiveHealthRegenTimer.RunTimer)
            passiveHealthRegenTimer.StopTimer();
        
        delayPassiveHealthRegenTimer.RestartTimer();


        // Actually take damage
        health.Decrease(damage);
    }

    public void TakeMaxDamage()
    {
        // nothing here, player should never take max damage
    }

    public void HealthPerkEnabled()
    {
        health.UpdateMax(20);
    }

    public void HealthPerkDisabled()
    {
        health.UpdateMax(10);
    }

    public void ShakeCam(float camShakeAmount, float camShakeDuration)
    {
        cam.TurnOnCamShake(camShakeAmount, camShakeDuration);
    }

    public void ExtraSFX(AudioClip _clip)
    {
        extraSFXSource.PlayOneShot(_clip);
    }
}