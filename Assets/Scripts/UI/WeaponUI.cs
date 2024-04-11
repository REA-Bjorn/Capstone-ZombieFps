using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;

    [SerializeField] private TextMeshProUGUI gunText;

    public void UpdateGunText()
    {
        gunText.text = WeaponManager.Instance.CurrentWeapon.GetComponent<WeaponBase>().Name;
    }

    public void UpdateAmmoText()
    {
        ammoText.text = WeaponManager.Instance.CurrentWeapon.GetComponent<WeaponBase>().Ammo.CurrentValue.ToString();
    }
}
