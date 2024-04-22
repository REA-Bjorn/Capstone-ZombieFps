using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTimer), typeof(WeaponAudio), typeof(Animator))]
public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected AttackPool attack;
    [SerializeField] protected AmmoPool ammo;
    [SerializeField] protected ReserveAmmoPool reserves;

    [Seperator]
    [SerializeField] protected float range;
    [SerializeField] protected CustomTimer fireRateTimer;
    [SerializeField] protected CustomTimer reloadTimer;

    [Seperator]
    [Header("Cosmetics/Information")]
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected Animator gunAnimations;
    [SerializeField] protected string gunName = "";
    [SerializeField] protected WeaponAudio audioScript;
    [SerializeField] protected WeaponType type;

    // Properties
    public string Name => gunName;
    public AmmoPool Ammo => ammo;
    public AttackPool Attack => attack;
    public ReserveAmmoPool Reserves => reserves;
    public float ShootDist => range;
    public WeaponType Type => type;

    protected bool CanUse;

    public virtual void Start()
    {
        Subscribers();

        attack.SetMax();
        ammo.SetMax();
        reserves.SetMax();

        UIManager.Instance.UpdateWeaponsUI();
        CanUse = false;
    }

    private void Subscribers()
    {
        fireRateTimer.OnStart += TimerStart;
        fireRateTimer.OnRestart += TimerStart;
    }

    public virtual void WeaponOn()
    {
        InputManager.Instance.Action.Reload.started += Reload;
        CanUse = true;
    }

    public virtual void OnDisable()
    {
        fireRateTimer.OnStart -= TimerStart;
        fireRateTimer.OnRestart -= TimerStart;

        InputManager.Instance.Action.Reload.started -= Reload;
    }

    private void TimerStart()
    {
        CanUse = false;
    }

    public virtual bool Shoot()
    {
        // Return if the fire rate timer is not running
        // meaning we can shoot again!

        return !fireRateTimer.RunTimer && !reloadTimer.RunTimer;
    }

    public void SetMaxAmmo()
    {
        ammo.SetMax();
        reserves.SetMax();
    }

    public void Reload(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Don't reload if we already reloading or the timer is running
        if (reloadTimer.RunTimer)
            return;

        reloadTimer.StartTimer();

        // If we have reserves
        if (reserves.IsValid && !ammo.IsMaxed)
        {
            gunAnimations?.SetTrigger("Reload");

            float used = ammo.Max - ammo.CurrentValue;

            // we know reserves has something
            if (reserves.CurrentValue > used)
            {
                ammo.Increase(used);
                reserves.Decrease(used);
            }
            else
            {
                ammo.Increase(reserves.CurrentValue);
                reserves.Decrease(reserves.CurrentValue);
            }

            UIManager.Instance.UpdateWeaponsUI();
        }
        else
        {
            UIManager.Instance.FlashWeaponsUI();
        }
    }

    protected void WeaponFX()
    {
        // Plays the muzzle flash animation
        muzzleFlash.Play();

        // Runs the timer for fire rate
        fireRateTimer.StartTimer();

        // Plays gun shot audio
        audioScript?.PlayShoot();

        // Use an ammo because we can shoot it
        ammo.Decrease(1f);
        UIManager.Instance.UpdateWeaponsUI();
    }
}
