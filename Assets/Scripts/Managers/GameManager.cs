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

    public const string KEY_MAINMENU = "MainMenuLevel";
    public const string KEY_LEVEL1 = "Level1";
    public const string KEY_LEVEL2 = "Level2";
    public const string KEY_LEVEL3 = "Level3";

    private int reviveBoughtCount = 0;

    public bool CanBuyRevive => (reviveBoughtCount <= 2);

    [SerializeField] private Transform playerSpawnPoint;

    private float storedTimeScale;

    public void RetryLevel()
    {
        UIManager.Instance.SceneFade.FadeTo(SceneManager.GetActiveScene().name);
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

        storedTimeScale = 1; // hard coded... need to find a better way to do this but for now our time scale will always be 1 regardless
        UIManager.Instance.TurnOffPauseMenu();

        PlayerBase.instance.transform.SetPositionAndRotation(playerSpawnPoint.position, playerSpawnPoint.rotation);
        playerTransfrom = PlayerBase.instance.transform;

    }

    public void BoughtARevive()
    {
        reviveBoughtCount++;
    }

    public void PlayerReviving()
    {
        WaveManager.Instance.KillAllAliveEnemies();
        PerkManager.Instance.ResetAllPerks();
        PlayerBase.instance.Health.SetMax();
    }

    public void UnPauseGame()
    {
        Time.timeScale = storedTimeScale;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        InputManager.Instance.UnPauseActions();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        InputManager.Instance.PauseActions();
    }

    public void QuitGame()
    {
        //UnPauseGame();
        UnPauseGame();
        UIManager.Instance.SceneFade.FadeTo(KEY_MAINMENU);
    }
}
