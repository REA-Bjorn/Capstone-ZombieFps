using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [Seperator]
    [SerializeField] private GameObject deathUI;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private PauseMenu pauseMenuScript;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("wa");
            TogglePauseMenu();
        }
    }

    private void TurnOffAllUI()
    {
        playerUI.SetActive(false);
        deathUI.SetActive(false);
        pauseMenu.SetActive(false);
        pauseMenuScript.TurnOff();
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            TurnOffPauseMenu();
        }
        else
        {
            TurnOnPauseMenu();
        }
    }

    public void TurnOffPauseMenu()
    {
        TurnOnPlayerMenu();
        GameManager.Instance.UnPauseGame();
    }

    public void TurnOnPauseMenu()
    {
        GameManager.Instance.PauseGame();
        TurnOffAllUI();
        pauseMenu.SetActive(true);
    }

    private void TurnOnPlayerMenu()
    {
        TurnOffAllUI();
        playerUI.SetActive(true);
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
        TurnOnPlayerMenu();
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
