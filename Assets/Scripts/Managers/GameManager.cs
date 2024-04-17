using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum Levels
{
    MainMenu,
    Level1,
    Level2,
    Level3
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Transform playerTransfrom;
    public Transform Player => PlayerBase.instance.transform;

    public const string KEY_MAINMENU = "MainMenu";
    public const string KEY_LEVEL1 = "Level1";
    public const string KEY_LEVEL2 = "Level2";
    public const string KEY_LEVEL3 = "Level3";

    [SerializeField] private Transform playerSpawnPoint;

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(Levels level)
    {
        switch (level)
        {
            case Levels.MainMenu:
                SceneManager.LoadScene(KEY_MAINMENU);
                break;
            case Levels.Level1:
                SceneManager.LoadScene(KEY_LEVEL1);
                break;
            case Levels.Level2:
                SceneManager.LoadScene(KEY_LEVEL2);
                break;
            case Levels.Level3:
                SceneManager.LoadScene(KEY_LEVEL3);
                break;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerBase.instance.transform.SetPositionAndRotation(playerSpawnPoint.position, playerSpawnPoint.rotation);
        playerTransfrom = PlayerBase.instance.transform;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void UnPauseGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
