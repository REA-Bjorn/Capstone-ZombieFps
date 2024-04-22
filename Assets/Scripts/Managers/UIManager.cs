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
    [Seperator]
    [SerializeField] private PlayerUI playerUIScript;
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private PauseMenu pauseMenuScript;
    [Seperator]
    [SerializeField] private GameObject minimapObject;

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
        pauseMenuObject.SetActive(true);
    }

    private void TurnOnPlayerMenu()
    {
        TurnOffAllUI();
        playerUIScript.TurnOnPlayerUI();
        minimapObject.SetActive(true);
    }

    public void DeathMenu()
    {
        death.SetActive(true);
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

    // Flash Waves
    public void FlashWaveCounter()
    {
        StartCoroutine(FlashWave());
    }

    private IEnumerator FlashWave()
    {
        yield return null;
        //float tickTime = 6;
        //while (tickTime > 0)
        //{
        //    tickTime -= Time.deltaTime;

        //    yield return null;
        //}

        //yield return new WaitForSeconds(1);
    }
}
