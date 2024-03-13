using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float enemyCount;
    public float enemyKill;

    public void IncEndGoal()
    {
        enemyCount++;
        UIManager.Instance.UpdateSum();
    }

    public void UpdateGameStatus()
    {
        enemyKill++;

        UIManager.Instance.UpdateSum();

        if (enemyKill == enemyCount)
        {
            PlayerBase.instance.LockPlayer();
            UIManager.Instance.WinMenu();
        }
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
