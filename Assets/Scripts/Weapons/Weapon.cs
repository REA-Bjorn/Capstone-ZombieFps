using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CustomTimer))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private AttackPool attack;
    [SerializeField] private AmmoPool ammo;
    [SerializeField] private ReserveAmmoPool reserves;

    [Seperator]
    [SerializeField] private float range;
    [SerializeField] private bool autoFire;
    [SerializeField] private CustomTimer fireRateTimer;
    [SerializeField] private CustomTimer reloadTimer;

    [Seperator]
    [Header("Cosmetics/Information")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Animator gunAnimations;
    [SerializeField] private string gunName = "";
    [SerializeField] private WeaponAudio audioScript;

    // Properties
    public string Name => gunName;
    public AmmoPool Ammo => ammo;
    public ReserveAmmoPool Reserves => reserves;

    private bool CanUse;
    public float ShootDist => range;

    void Start()
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
        fireRateTimer.OnEnd += TimerEnd;
        fireRateTimer.OnStart += TimerStart;
        fireRateTimer.OnRestart += TimerStart;
    }

    private void StoppedShooting(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        CanUse = true;
    }

    public void WeaponOn()
    {
        InputManager.Instance.Action.Reload.started += Reload;

        CanUse = true;

        if (!autoFire)
        {
            InputManager.Instance.Action.Attack.canceled += StoppedShooting;
        }
    }

    private void OnDisable()
    {
        fireRateTimer.OnEnd -= TimerEnd;
        fireRateTimer.OnStart -= TimerStart;
        fireRateTimer.OnRestart -= TimerStart;

        InputManager.Instance.Action.Reload.started -= Reload;

        if (!autoFire)
        {
            InputManager.Instance.Action.Attack.canceled -= StoppedShooting;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && autoFire)
        {
            Shoot();
        }
    }

    private void TimerStart()
    {
        CanUse = false;
    }

    private void TimerEnd()
    {
        if (autoFire)
        {
            CanUse = true;
        }
    }

    public void Shoot()
    {
        if (fireRateTimer.RunTimer)
            return;

        //nothing should be here
        // We can shoot the weapon b/c of fire rate
        // and we have ammo we can use ( > 1 )
        if (CanUse && ammo.IsValid)
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

            // Create Raycast Hit data
            RaycastHit hit;
            // Shoot a ray from the screen with our shoot dist
            // COD World At War also does it from the camera (tested this in-game)
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, ShootDist))
            {
                // Get an IDamage component from the hit object
                IDamage damage = hit.collider.GetComponent<IDamage>();
                if (damage != null)
                {
                    // if we hit something and that hit has the IDamage component
                    // call its take damage function
                    if (WeaponManager.Instance.InstaKill)
                    {
                        damage.TakeMaxDamage();
                    }
                    else
                    {
                        damage.TakeDamage(attack.CurrentValue);
                    }
                }
            }
        }
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
            StartCoroutine(UIManager.Instance.FlashWeaponsUI());
        }
    }
}
