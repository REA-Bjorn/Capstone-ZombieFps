using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject pointsObj;
    [SerializeField] private GameObject waveObj;
    [SerializeField] private GameObject crossHairObj;
    [SerializeField] private GameObject weaponsObj;
    
    [Seperator]
    [SerializeField] private TextMeshProUGUI totalScore;
    [SerializeField] private TextMeshProUGUI totalWaves;

    public TextMeshProUGUI WaveText => totalWaves;
    
    [Seperator]
    [SerializeField] private Image crossHair;
    
    [Seperator]
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI reservedAmmoText;
    [SerializeField] private TextMeshProUGUI gunText;

    public void StartUp()
    {
        PointsManager.Instance.OnPointsChanged += UpdateScore;
    }

    private void OnDestroy()
    {
        PointsManager.Instance.OnPointsChanged -= UpdateScore;
    }

    public void TurnOff()
    {
        pointsObj.SetActive(false);
        waveObj.SetActive(false);
        crossHairObj.SetActive(false);
        weaponsObj.SetActive(false);
    }

    public void TurnOnPlayerUI()
    {
        pointsObj.SetActive(true);
        waveObj.SetActive(true);
        crossHairObj.SetActive(true);
        weaponsObj.SetActive(true);

        UpdateScore();
        UpdateWavesText();
        UpdateWeaponUI();
    }

    // Points UI Specific Function
    public void UpdateScore()
    {
        totalScore.text = PointsManager.Instance.GetPoints().ToString();
    }

    public void UpdateWavesText()
    {
        totalWaves.text = WaveManager.Instance.CurrentWave.ToString();
    }

    // Weapon Specific Functions
    public void UpdateWeaponUI()
    {
        ammoText.text = WeaponManager.Instance.CurrAmmoTxt;
        reservedAmmoText.text = WeaponManager.Instance.CurrReserveTxt;
        gunText.text = WeaponManager.Instance.GunName;
    }

    public IEnumerator FlashWeaponsUI()
    {
        ammoText.color = UnityEngine.Color.red;
        reservedAmmoText.color = UnityEngine.Color.red;
        yield return new WaitForSeconds(0.25f);
        ammoText.color = UnityEngine.Color.white;
        reservedAmmoText.color = UnityEngine.Color.white;
    }

    // Crosshair Specific Functions
    public void ChangeCrosshairColor(UnityEngine.Color _color)
    {
        crossHair.color = _color;
    }
}
