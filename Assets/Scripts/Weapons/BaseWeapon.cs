using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

    [Seperator]
    [SerializeField] protected List<GameObject> subObjects;

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

    private float storedReload = 0f;
    private float storedFireRate = 0f;

    public virtual void Start()
    {
        attack.SetMax();
        ammo.SetMax();
        reserves.SetMax();

        UIManager.Instance.UpdateWeaponsUI();
        CanUse = false;

        storedReload = reloadTimer.DurationTime;
        storedFireRate = fireRateTimer.DurationTime;
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
        // Return if the fire rate timer is not running or reload timer is running or the UI is paused
        if (fireRateTimer.RunTimer || reloadTimer.RunTimer || UIManager.Instance.IsUIPause)
        {
            return false;
        }

        // Check if ammo is empty
        if (ammo.CurrentValue <= 0)
        {
            // Trigger the reload method
            Reload(new UnityEngine.InputSystem.InputAction.CallbackContext());
            return false;
        }

        // Proceed with shooting
        WeaponFX();
        return true;
    }

    public void SetMaxAmmo()
    {
        ammo.SetMax();
        reserves.SetMax();
    }

    public virtual void Reload(UnityEngine.InputSystem.InputAction.CallbackContext context = default)
    {
        // Don't reload if we already reloading or the timer is running
        if (reloadTimer.RunTimer)
            return;

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

            audioScript.PlayReload();
            UIManager.Instance.UpdateWeaponsUI();

            if (PerkManager.Instance.FasterReload)
            {
                reloadTimer.StartTimer(storedReload / 2);
            }
            else
            {
                reloadTimer.StartTimer(storedReload);
            }
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

        if (PerkManager.Instance.FasterShoot)
        {
            fireRateTimer.StartTimer(storedFireRate / 2);
        }
        else
        {
            fireRateTimer.StartTimer();
        }

        // Plays gun shot audio
        audioScript?.PlayShoot();

        // Player Shoot Animation
        gunAnimations.SetTrigger("Shoot");

        // Use an ammo because we can shoot it
        if(ammo.CurrentValue > 0)
        {
            ammo.Decrease(1f);
            UIManager.Instance.UpdateWeaponsUI();
        }
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

        UpdateLayers(false);
    }

    public void UpdateLayers(bool SwitchToWeaponLayer)
    {
        if (SwitchToWeaponLayer)
        {
            foreach (GameObject obj in subObjects)
            {
                obj.layer = 12;
            }
        }
        else
        {
            foreach (GameObject obj in subObjects)
            {
                obj.layer = 0;
            }
        }
    }

}

