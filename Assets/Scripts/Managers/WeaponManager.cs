using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Search;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    private GameObject currWeapon;

    [SerializeField] private GameObject Primary;
    [SerializeField] private GameObject Secondary;

    public float ShootDist
    {
        get
        {
            float val = 0;

            if (currWeapon != null && currWeapon.GetComponent<WeaponBase>() != null)
            {
                val = currWeapon.GetComponent<WeaponBase>().ShootDist;
            }

            return val;
        }
    }

    public GameObject CurrentWeapon => currWeapon;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currWeapon = Primary;
    }

    public void Shoot()
    {
        if (currWeapon != null)
        {
            currWeapon.GetComponent<WeaponBase>().Shoot();
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
    }

    void EnableSecondary()
    {
        currWeapon = Secondary;
        Secondary.SetActive(true);
        Primary.SetActive(false);
    }

}
