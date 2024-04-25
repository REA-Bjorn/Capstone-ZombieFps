using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button optionsBtn;
    [SerializeField] private Button contolsBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button quitBtn;

    [SerializeField] private Button optionsReturnButton;
    [SerializeField] private Button controlsReturnButton;

    [Seperator]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;

    private void Start()
    {
        AddListeners();
    }

    public void ClickedOptionsBtn()
    {
        TurnOff();
        optionsMenu.SetActive(true);
    }

    public void ClickedControlsBtn()
    {
        TurnOff();
        controlsMenu.SetActive(true);
    }

    public void ClickedResume()
    {
        UIManager.Instance.TogglePauseMenu();
    }

    public void ClickedQuit()
    {
        GameManager.Instance.QuitGame();
    }
    
    private void ReturnToPauseMenu()
    {
        UIManager.Instance.TurnOnPauseMenu();
    }

    private void ClickedRestart()
    {
        GameManager.Instance.RetryLevel();
    }

    private void AddListeners()
    {
        resumeBtn.onClick.AddListener(ClickedResume);
        optionsBtn.onClick.AddListener(ClickedOptionsBtn);
        contolsBtn.onClick.AddListener(ClickedControlsBtn);
        restartBtn.onClick.AddListener(ClickedRestart);
        quitBtn.onClick.AddListener(ClickedQuit);
        optionsReturnButton.onClick.AddListener(ReturnToPauseMenu);
        controlsReturnButton.onClick.AddListener(ReturnToPauseMenu);
    }

    public void TurnOff()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }
}
