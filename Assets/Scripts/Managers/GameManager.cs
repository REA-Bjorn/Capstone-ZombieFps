using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform playerTransfrom;
    public Transform Player => playerTransfrom;

    public float enemyCount;
    public float enemyKill;

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        instance = this;
    }
}
