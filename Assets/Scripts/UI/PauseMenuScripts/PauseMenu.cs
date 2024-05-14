using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button optionsBtn;
    [SerializeField] private Button keyboardBtn;
    [SerializeField] private Button controllerBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button quitBtn;

    [SerializeField] private Button optionsReturnButton;
    [SerializeField] private Button controlsKeyboardReturnButton;
    [SerializeField] private Button controlsControllerReturnButton;

    [Seperator]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject keybindsMenu;
    [SerializeField] private GameObject controllerMenu;

    [Seperator]
    [Header("Menu Objects")]
    [SerializeField] GameObject audioSelectedObj;
    [SerializeField] GameObject graphicsSelectedObj;
    [SerializeField] GameObject cameraSelectedObj;
    [SerializeField] GameObject keybindsSelectedObj;
    [SerializeField] GameObject controllerSelectedObj;
    [SerializeField] GameObject optionsSelectedObj;
    [SerializeField] GameObject pauseMenuSelectedObj;
    [SerializeField] GameObject deathMenuSelectedObj;
   
    private void Start()
    {
        AddListeners();
    }

    public void ClickedOptionsBtn()
    {
        TurnOff();
        optionsMenu.SetActive(true);
        DisplayOptions();
    }

    public void ClickKeybindsBtn()
    {
        TurnOff();
        keybindsMenu.SetActive(true);
        DisplayKeybinds();
    }

    public void ClickControllerBtn()
    {
        TurnOff();
        controllerMenu.SetActive(true);
        DisplayController();
    }

    public void ClickedResume()
    {
        UIManager.Instance.TogglePauseMenu();
    }

    public void ClickedQuit()
    {
        GameManager.Instance.QuitGame();
    }
    
    public void ReturnToPauseMenu()
    {
        UIManager.Instance.TurnOnPauseMenu();
        DisplayPauseMenuEventSystem();
    }

    private void ClickedRestart()
    {
        GameManager.Instance.RetryLevel();
    }

    private void AddListeners()
    {
        resumeBtn.onClick.AddListener(ClickedResume);
        optionsBtn.onClick.AddListener(ClickedOptionsBtn);
        keyboardBtn.onClick.AddListener(ClickKeybindsBtn);
        controllerBtn.onClick.AddListener(ClickControllerBtn);
        restartBtn.onClick.AddListener(ClickedRestart);
        quitBtn.onClick.AddListener(ClickedQuit);

        optionsReturnButton.onClick.AddListener(ReturnToPauseMenu);
        controlsKeyboardReturnButton.onClick.AddListener(ReturnToPauseMenu);
        controlsControllerReturnButton.onClick.AddListener(ReturnToPauseMenu);
    }

    public void TurnOff()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        keybindsMenu.SetActive(false);
        controllerMenu.SetActive(false);
    }

    public void DisplayOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsSelectedObj);
    }

    public void DisplayPauseMenuEventSystem()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuSelectedObj);
    }

    public void DisplayAudio()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(audioSelectedObj);
    }

    public void DisplayGraphics()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(graphicsSelectedObj);
    }

    public void DisplayCamera()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(cameraSelectedObj);
    }

    public void DisplayKeybinds()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(keybindsSelectedObj);
    }

    public void DisplayController()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controllerSelectedObj);
    }

    public void DisplayDeath()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(deathMenuSelectedObj);
    }
}
