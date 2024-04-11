using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI reservedAmmoText;
    [SerializeField] private TextMeshProUGUI gunText;

    public void UpdateUI()
    {
        ammoText.text = WeaponManager.Instance.CurrAmmoTxt;
        reservedAmmoText.text = WeaponManager.Instance.CurrReserveTxt;
        gunText.text = WeaponManager.Instance.GunName;
    }

    public void ColorAmmo(Color color)
    {
        ammoText.color = color;
        reservedAmmoText.color = color;
    }
}
