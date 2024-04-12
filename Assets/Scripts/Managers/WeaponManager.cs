using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Search;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    [SerializeField] private Weapon currWeapon;
    [SerializeField] private Weapon Primary;
    [SerializeField] private Weapon Secondary;

    [SerializeField] private CustomTimer instaKillTimer;

    private bool instaKillStatus = false;
    public bool InstaKill => instaKillStatus;
    public float ShootDist
    {
        get
        {
            float val = 0;

            if (currWeapon != null && currWeapon != null)
            {
                val = currWeapon.ShootDist;
            }

            return val;
        }
    }

    public Weapon CurrentWeapon => currWeapon;
    public string GunName => currWeapon?.Name;
    public string CurrAmmoTxt => currWeapon?.Ammo.CurrentValue.ToString();
    public string CurrReserveTxt => currWeapon?.Reserves.CurrentValue.ToString();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        instaKillTimer.OnStart += InstaKillTimer_OnStart;
        instaKillTimer.OnEnd += InstaKillTimer_OnEnd;

        currWeapon = Primary;
        currWeapon.WeaponOn();
        UIManager.Instance.UpdateWeaponsUI();
    }

    private void InstaKillTimer_OnStart()
    {
        instaKillStatus = true;
    }

    private void InstaKillTimer_OnEnd()
    {
        instaKillStatus = false;
    }

    // Update is called once per frame
    public void ToggleWeapon(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (Mathf.Abs(InputManager.Instance.ScrollVect.y) > 0f)
        {
            if (Primary != null && currWeapon == Secondary)
            {
                EnablePrimary();
            }
            else if (Secondary != null)
            {
                EnableSecondary();
            }
        }

        currWeapon.WeaponOn();
        UIManager.Instance.UpdateWeaponsUI();
    }

    public void AddWeapon(GameObject _weapon)
    {
        //_weapon.transform.SetLocalPositionAndRotation(currWeapon.transform.position, currWeapon.transform.rotation);

        if (Secondary == null)
        {
            Secondary = _weapon.GetComponent<Weapon>();
            EnableSecondary();
        }
        else if (currWeapon == Primary)
        {
            Destroy(currWeapon);
            Primary = _weapon.GetComponent<Weapon>();
            EnablePrimary();
        }
        else if (currWeapon == Secondary)
        {
            Destroy(currWeapon);
            Secondary = _weapon.GetComponent<Weapon>();
            EnableSecondary();
        }

        currWeapon.transform.SetParent(gameObject.transform);
        _weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        UIManager.Instance.UpdateWeaponsUI();
    }

    void EnablePrimary()
    {
        currWeapon = Primary;
        Primary.gameObject.SetActive(true);
        Secondary.gameObject.SetActive(false);
        currWeapon.WeaponOn();
    }

    void EnableSecondary()
    {
        currWeapon = Secondary;
        Secondary.gameObject.SetActive(true);
        Primary.gameObject.SetActive(false);
        currWeapon.WeaponOn();
    }

    public void DisableWeapon()
    {
        currWeapon = null;
    }

    public void EnableWeapon()
    {
        if (Primary != null)
        {
            currWeapon = Primary;
        }
        else if (Secondary != null)
        {
            currWeapon = Secondary;
        }
    }

    private void OnDestroy()
    {
        instaKillTimer.OnStart -= InstaKillTimer_OnStart;
        instaKillTimer.OnEnd -= InstaKillTimer_OnEnd;
    }

    public void RefillAllWeapon()
    {
        Primary?.SetMaxAmmo();
        Secondary?.SetMaxAmmo();
    }

    public void EnableInstaKill()
    {
        instaKillTimer.StartTimer();
    }

    public void Shoot(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        currWeapon?.Shoot();
    }
}
