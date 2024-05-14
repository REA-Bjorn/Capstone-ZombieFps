using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject death;
    [SerializeField] private DeathMenu deathUI;
    [Seperator]
    [SerializeField] private PlayerUI playerUIScript;
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private PauseMenu pauseMenuScript;
    [SerializeField] private PerkUI perkUI;
    [SerializeField] private PlayerHit hitUI;
    [Seperator]
    [SerializeField] private GameObject minimapObject;
    [SerializeField] private Animator waveAnimation;
    [SerializeField] private SceneFader fader;

    public PerkUI PerkUIScript => perkUI;
    public PlayerUI PlayerUIScript => playerUIScript;
    public PlayerHit PlayerHitScript=> hitUI;
    public SceneFader SceneFade => fader;

    private bool isUIPause = true;
    public bool IsUIPause => isUIPause;

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        TurnOnPlayerMenu();
        playerUIScript.StartUp();
    }

    private void TurnOffAllUI()
    {
        playerUIScript.TurnOff();
        pauseMenuScript.TurnOff();
        pauseMenuObject.SetActive(false);
        minimapObject.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        if (pauseMenuObject.activeSelf)
        {
            TurnOffPauseMenu();
        }
        else
        {
            TurnOnPauseMenu();
            pauseMenuScript.ReturnToPauseMenu();
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
        isUIPause = true;
        pauseMenuObject.SetActive(true);
    }

    private void TurnOnPlayerMenu()
    {
        TurnOffAllUI();
        isUIPause = false;
        playerUIScript.TurnOnPlayerUI();
        minimapObject.SetActive(true);
    }

    public void DeathMenu()
    {
        TurnOffAllUI();
        death.SetActive(true);
        GameManager.Instance.PauseGame();
        pauseMenuScript.DisplayDeath();
        deathUI.UpdateTotalScore();
    }

    public void UpdateWeaponsUI()
    {
        playerUIScript.UpdateWeaponUI();
    }

    public void FlashWeaponsUI()
    {
        StartCoroutine(playerUIScript.FlashWeaponsUI());
    }

    public void UpdateCrosshair(Color color)
    {
        playerUIScript.ChangeCrosshairColor(color);
    }

    public void PauseMenu(InputAction.CallbackContext context)
    {
        TogglePauseMenu();
    }

    public void FlashWaveCounter()
    {
        waveAnimation.SetTrigger("FlashWave");
    }
}
