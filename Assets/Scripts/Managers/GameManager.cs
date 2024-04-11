using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Transform playerTransfrom;
    public Transform Player => PlayerBase.instance.transform;

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
        PlayerBase.instance.transform.SetPositionAndRotation(playerSpawnPoint.position, playerSpawnPoint.rotation);
        playerTransfrom = PlayerBase.instance.transform;
        //playerTransfrom = playerSpawnPoint;
    }
}
