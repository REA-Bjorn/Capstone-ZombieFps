using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Need to use these a lot
    public MeshRenderer meshRenderer;
    public Material matOne;
    public Material matTwo;

    // Properties
    public string Name => gunName;
    public AmmoPool Ammo => ammo;
    public AttackPool Attack => attack;
    public ReserveAmmoPool Reserves => reserves;
    public float ShootDist => range;
    public WeaponType Type => type;

    protected bool CanUse;

    private GameObject parentStand;
    private WeaponUnlockStand weaponStand;

    // TODO: Create 2 private float variables for storing the reload and fireRate durations

    public virtual void Start()
    {
        attack.SetMax();
        ammo.SetMax();
        reserves.SetMax();

        UIManager.Instance.UpdateWeaponsUI();
        CanUse = false;

        // TODO: set stored reload speed variable to the reload timer's DurationTime
        
        // TODO: set stored fire rate variable to the fire rate timer's DurationTime
    }

    public virtual void WeaponOn()
    {
        fireRateTimer.OnStart += TimerStart;
        fireRateTimer.OnRestart += TimerStart;
        fireRateTimer.OnEnd += TimerEnd;
        InputManager.Instance.Action.Reload.started += Reload;

        gunAnimations.SetTrigger("Unholster");
        audioScript.PlayUnholster();
        CanUse = true;
    }

    public virtual void OnDisable()
    {
        audioScript.PlayHolster();
        gunAnimations.SetTrigger("Holster");
        fireRateTimer.OnStart -= TimerStart;
        fireRateTimer.OnRestart -= TimerStart;
        fireRateTimer.OnEnd -= TimerEnd;

        InputManager.Instance.Action.Reload.started -= Reload;
    }

    private void TimerEnd()
    {
        CanUse = true;
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

        // If we have reserves
        if (reserves.IsValid && !ammo.IsMaxed)
        {
            // PERK TODO: Faster Reload
            // (do not create any new functions)
            // you should only be calling:
            //      perk's boolean variable from PerkManager,
            //      the stored reload speed variable,
            //      and the start timer function from the reload timer

            // If the "FasterReload" perk is enabled
            // call the reload timer's start timer function and pass in the stored reload speed / 2
            // otherwise call the reload timer's start timer function and pass in the stored reload speed (this way its not 2x faster)

            // Remove this line when done or comment it out
            reloadTimer.StartTimer();

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

            audioScript.PlayReload();
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

        // PERK TODO: FireRate
        // (do not create any new functions)
        // you should only be calling:
        //      perk's boolean variable from PerkManager,
        //      the stored fire rate variable,
        //      and the start timer function from the fireRate Timer

        // If the "FasterReload" perk is enabled
        // call the fireRate Timer's start timer function and pass in the stored fire rate / 2
        // otherwise call the fireRate Timer's start timer function and pass in the stored fire rate (this way its not 2x faster)

        // Remove this line when done or comment it out
        // Runs the timer for fire rate
        fireRateTimer.StartTimer();

        // Plays gun shot audio
        audioScript?.PlayShoot();

        // Player Shoot Animation
        gunAnimations.SetTrigger("Shoot");

        // Use an ammo because we can shoot it
        ammo.Decrease(1f);
        UIManager.Instance.UpdateWeaponsUI();
    }

    public void SetParentStandObj(GameObject _obj, WeaponUnlockStand _stand)
    {
        parentStand = _obj;
        weaponStand = _stand;
    }

    public void SendBackToParentStand()
    {
        if (parentStand != null)
        {
            transform.parent = parentStand.transform;
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            weaponStand.UnbuyWeaponFunctionality();

            ammo.SetMax();
            reserves.SetMax();

            fireRateTimer.OnStart -= TimerStart;
            fireRateTimer.OnRestart -= TimerStart;
            fireRateTimer.OnEnd -= TimerEnd;
            InputManager.Instance.Action.Reload.started -= Reload;

            CanUse = false;
        }
        else // This if for the default revolver
        {
            transform.parent = null;
            transform.SetLocalPositionAndRotation(new Vector3(-50, -50, -50), Quaternion.identity);

            fireRateTimer.OnStart -= TimerStart;
            fireRateTimer.OnRestart -= TimerStart;
            fireRateTimer.OnEnd -= TimerEnd;
            InputManager.Instance.Action.Reload.started -= Reload;

            CanUse = false;
        }
    }
}
