using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    [SerializeField] private PlayerAudio aud;
    [Seperator]
    [SerializeField] private AudioSource extraSFXSource;
    [Seperator]
    [SerializeField] private CustomTimer passiveHealthRegenTimer;
    [SerializeField] private CustomTimer delayPassiveHealthRegenTimer;

    // Properties
    public SpeedPool Spd => speed;
    public HealthPool Health => health;
    public StaminaPool Stam => stamina;
    public float ShootDist => WeaponManager.Instance.ShootDist;

    private float iFrameCounter = 0;
    private bool iFrameOn = false;

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

        delayPassiveHealthRegenTimer.OnEnd += () =>
        {
            passiveHealthRegenTimer.StartTimer();
        };

        passiveHealthRegenTimer.OnTick += () =>
        {
            health.Increase(Time.deltaTime * 0.1f); // time.delta time will be a lot so 0.1 times that

            if (health.CurrentValue > health.Max / 2f)
            {
                UIManager.Instance.PlayerHitScript.ToggleHalfHealthVisual(false);
            }

            if (health.IsMaxed)
                passiveHealthRegenTimer.StopTimer();
        };
    }

    private void HealthDepleted()
    {
        if (PerkManager.Instance.SecondaryLife)
        {
            GameManager.Instance.PlayerReviving();
            speed.SetMax();
            health.SetMax();
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

        delayPassiveHealthRegenTimer.OnEnd -= () =>
        {
            passiveHealthRegenTimer.StartTimer();
        };

        passiveHealthRegenTimer.OnTick -= () =>
        {
            health.Increase(Time.deltaTime * 0.1f);

            if (health.CurrentValue > health.Max / 2f)
            {
                UIManager.Instance.PlayerHitScript.ToggleHalfHealthVisual(false);
            }

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

        if (iFrameOn)
        {
            iFrameCounter++;

            if (iFrameCounter >= 30)
            {
                iFrameCounter = 0;
                iFrameOn = false;
            }
        }
    }

    public void TakeDamage(float damage, bool forceKilled = false)
    {
        if (!iFrameOn)
        {
            iFrameOn = true;
            
            // Visual UX
            UIManager.Instance.PlayerHitScript.Active();
            // Audio UX
            aud.PlayHitSFX();

            // Timer code for regen resetting
            if (passiveHealthRegenTimer.RunTimer)
                passiveHealthRegenTimer.StopTimer();

            delayPassiveHealthRegenTimer.RestartTimer();

            // Actually take damage
            health.Decrease(damage);

            if (health.CurrentValue <= health.Max / 2f)
            {
                UIManager.Instance.PlayerHitScript.ToggleHalfHealthVisual(true);
            }
        }
    }

    public void TakeMaxDamage()
    {
        // nothing here, player should never take max damage
    }

    public void HealthPerkEnabled()
    {
        health.UpdateMax(Mathf.Floor(health.Max * 2.5f));
        
        if (health.CurrentValue <= health.Max / 2f)
        {
            UIManager.Instance.PlayerHitScript.ToggleHalfHealthVisual(true);
        }
    }

    public void HealthPerkDisabled()
    {
        health.UpdateMax(3);
        health.SetMax();
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