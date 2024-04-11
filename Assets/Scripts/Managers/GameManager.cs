using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Transform playerTransfrom;
    public Transform Player => playerTransfrom;

    [SerializeField] private Transform playerSpawnPoint;

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerTransfrom = PlayerBase.instance.transform;
        playerTransfrom = playerSpawnPoint;
    }
}
