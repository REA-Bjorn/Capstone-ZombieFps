using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUnlockStand : MonoBehaviour, IInteractable
{
    [SerializeField] private WeaponType weaponType;
    [Seperator]
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private Collider coll;

    private WeaponHolder weaponHold;

    private GameObject weaponObj;

    public void Interact()
    {
        if (PointsManager.Instance.CurrPts >= weaponHold.Cost)
        {
            PointsManager.Instance.RemovePoints(weaponHold.Cost);
            WeaponManager.Instance.AddWeapon(weaponObj);
            coll.enabled = false;
        }
    }

    void Start()
    {
        if (weaponType == WeaponType.NULLED)
        {
            coll.enabled = false;
            return;
        }

        weaponHold = WeaponPoolManager.Instance.GetGunGO(weaponType);
        weaponObj = Instantiate(weaponHold.WeaponPrefab, weaponPoint);

        textDisplay.text = weaponType.ToString() + "\n$" + weaponHold.Cost.ToString();
        UpdateTextColor();

        PointsManager.Instance.OnPointsChanged += UpdateTextColor;
    }

    private void OnDestroy()
    {
        PointsManager.Instance.OnPointsChanged -= UpdateTextColor;
    }

    private void UpdateTextColor()
    {
        if (PointsManager.Instance.CurrPts >= weaponHold.Cost)
        {
            textDisplay.color = Color.green;
        }
        else
        {
            textDisplay.color = Color.red;
        }
    }
}
