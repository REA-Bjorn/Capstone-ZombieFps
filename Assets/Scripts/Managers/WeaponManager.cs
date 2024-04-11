using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Search;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    [SerializeField] private GameObject currWeapon;
    [SerializeField] private GameObject Primary;
    [SerializeField] private GameObject Secondary;

    [SerializeField] private CustomTimer instaKillTimer;

    private bool instaKillStatus = false;
    public bool InstaKill => instaKillStatus;
    public float ShootDist
    {
        get
        {
            float val = 0;

            if (currWeapon != null && currWeapon.GetComponent<Weapon>() != null)
            {
                val = currWeapon.GetComponent<Weapon>().ShootDist;
            }

            return val;
        }
    }

    public GameObject CurrentWeapon => currWeapon;
    public string GunName => currWeapon.GetComponent<Weapon>().Name;
    public string CurrAmmoTxt => currWeapon.GetComponent<Weapon>().Ammo.CurrentValue.ToString();
    public string CurrReserveTxt => currWeapon.GetComponent<Weapon>().Reserves.CurrentValue.ToString();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        instaKillTimer.OnStart += InstaKillTimer_OnStart;
        instaKillTimer.OnEnd += InstaKillTimer_OnEnd;

        currWeapon = Primary;
        currWeapon.GetComponent<Weapon>().WeaponOn();
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

    public void Shoot(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (currWeapon != null)
        {
            currWeapon.GetComponent<Weapon>().Shoot();
        }
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

        UIManager.Instance.UpdateWeaponsUI();
    }

    public void AddWeapon(GameObject _weapon)
    {
        _weapon.transform.SetLocalPositionAndRotation(currWeapon.transform.position, currWeapon.transform.rotation);

        if (Secondary == null)
        {
            Secondary = _weapon;
            EnableSecondary();
        }
        else if (currWeapon == Primary)
        {
            Destroy(currWeapon);
            Primary = _weapon;
            EnablePrimary();
        }
        else if (currWeapon == Secondary)
        {
            Destroy(currWeapon);
            Secondary = _weapon;
            EnableSecondary();
        }

        currWeapon.transform.SetParent(gameObject.transform);
    }

    void EnablePrimary()
    {
        currWeapon = Primary;
        Primary.SetActive(true);
        Secondary.SetActive(false);
        currWeapon.GetComponent<Weapon>().WeaponOn();
    }

    void EnableSecondary()
    {
        currWeapon = Secondary;
        Secondary.SetActive(true);
        Primary.SetActive(false);
        currWeapon.GetComponent<Weapon>().WeaponOn();
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
        Primary.GetComponent<Weapon>().SetMaxAmmo();
        Secondary.GetComponent<Weapon>().SetMaxAmmo();
    }

    public void EnableInstaKill()
    {
        instaKillTimer.StartTimer();
    }
}
