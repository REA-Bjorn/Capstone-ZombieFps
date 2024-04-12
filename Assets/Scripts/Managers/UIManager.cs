using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject death;
    [SerializeField] private GameState gameState;
    [SerializeField] private WeaponUI weaponUIScript;
    [Seperator]
    [SerializeField] private PlayerHit hitUI;
    [SerializeField] private CrosshairHover crosshairScript;
    [SerializeField] private GameObject playerUI;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore()
    {
        gameState.UpdateScore();
    }

    public void UpdateWaveCounter()
    {
        // update the text here for that
        // use WaveManager.Instance.CurrentWave;
    }

    public void DeathMenu()
    {
        death.SetActive(true);
        hitUI.PlayerDiedVignette();
    }

    // Start is called before the first frame update
    void Start()
    {
        death.SetActive(false);
    }

    public void UpdateWeaponsUI()
    {
        weaponUIScript.UpdateUI();
    }

    public IEnumerator FlashWeaponsUI()
    {
        weaponUIScript.ColorAmmo(Color.red);
        yield return new WaitForSeconds(0.25f);
        weaponUIScript.ColorAmmo(Color.white);
    }

    public void UpdateCrosshair(Color color)
    {
        crosshairScript.ChangeCrosshairColor(color);
    }

    public void HitFlash()
    {
        hitUI.Active();
    }

    public void LockUI()
    {
        playerUI.SetActive(false);
    }
}
