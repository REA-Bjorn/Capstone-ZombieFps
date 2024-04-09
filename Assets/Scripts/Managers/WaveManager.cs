using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Tooltip("The starting number of objects to spawn.")]
    [SerializeField] private int SpawnLimit = 6;
    [Tooltip("The Enemy Prefab to use when spawning.")]
    [SerializeField] GameObject enemyPrefab;

    [Seperator]
    [SerializeField] private CustomTimer waveCountdownTimer;

    // Spawn point collection
    private List<GameObject> spawnPoints = new List<GameObject>();
    // All enemies spawned
    private List<GameObject> enemyPool = new List<GameObject>();

    // Goal Trackers
    private int currentEnemyGoal = 0;
    private int currentEnemyGoalStored = 0;

    private void Start()
    {
        FindAllSpawnPoints();
        InstantiateEnemies();

        // Now setup rest of data
        SubscribeEvents();

        // Start the countdown to spawn those enemies!
        waveCountdownTimer.StartTimer();
    }

    private void InstantiateEnemies()
    {
        // Instantiate Enemies
        for (int i = 0; i < SpawnLimit; i++)
        {
            // Instantiate enemy
            GameObject spawnedEnemy = Instantiate(enemyPrefab, GetRandomSpawnPoint());
            spawnedEnemy.SetActive(false);

            // Add enemy to the list
            enemyPool.Add(spawnedEnemy);
        }
    }

    private void FindAllSpawnPoints()
    {
        // Find all spawn points for enemies
        var points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject point in points)
        {
            spawnPoints.Add(point);
            Debug.Log(point.transform.name);
        }
    }

    private void SubscribeEvents()
    {
        // Wave Countdown Timer events
        waveCountdownTimer.OnStart += WaveCountdownStarted;
        waveCountdownTimer.OnTick += WaveCountdownTicked;
        waveCountdownTimer.OnEnd += WaveCountdownEnded;

    }

    private void OnDestroy()
    {
        // Wave Countdown Timer events
        waveCountdownTimer.OnStart -= WaveCountdownStarted;
        waveCountdownTimer.OnTick -= WaveCountdownTicked;
        waveCountdownTimer.OnEnd -= WaveCountdownEnded;

    }

    private void SpawnEnemyEnded()
    {
        for (int i = 0; i < enemyPool.Count; ++i)
        {
            // enable the first deactivated enemy
            if (!enemyPool[i].activeInHierarchy)
            {
                EnableEnemy(enemyPool[i]);
                return;
            }
        }
    }

    private void WaveCountdownStarted()
    {
        // Call User Interfaces wave started logic here if needed
    }

    private void WaveCountdownTicked()
    {
        // Logic if we to call any functions during the wave countdown
    }

    private void WaveCountdownEnded()
    {
        Debug.Log("Starting wave!");
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        // Update Total Enemies to kill
        currentEnemyGoalStored += Random.Range(1, 5);
        currentEnemyGoal = currentEnemyGoalStored;

        int loopFor = currentEnemyGoal < SpawnLimit ? currentEnemyGoal : SpawnLimit;
        Debug.Log("GOING TO SPAWN:::::::: " + loopFor + " NEED TO SPAWN " + currentEnemyGoal);

        for (int i = 0; i < loopFor; ++i)
        {
            // Spawn the starting amount of enemies
            // - When enemies die, the wave manager decides if it should enable another enemy.
            SpawnEnemyEnded();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void EnableEnemy(GameObject _enemy)
    {
        _enemy.SetActive(true);
        _enemy.GetComponent<EnemyBase>().SpawnMe(); // handles maxing the stats for us
        _enemy.transform.position = GetRandomSpawnPoint().position;
    }

    public void EnemyKilled()
    {
        --currentEnemyGoal; // subtract enemies
        if (currentEnemyGoal - TotalCurrentlyUndead() + 1 > 0)
        {
            Debug.Log("STILL NEED TO SPAWN SOME: " + (currentEnemyGoal - TotalCurrentlyUndead()));
            // we still have enemies to kill
            SpawnEnemyEnded();
        }
        else if (currentEnemyGoal == 0)
        {
            waveCountdownTimer.StartTimer();
        }
    }


    private Transform GetRandomSpawnPoint()
    {
        int idx = Random.Range(0, spawnPoints.Count);
        return spawnPoints[idx].transform;
    }

    private int TotalCurrentlyUndead()
    {
        int currentlyUndead = 0;

        foreach (GameObject enemy in enemyPool)
        {
            if (enemy.activeInHierarchy)
            {
                ++currentlyUndead;
            }
        }
        return currentlyUndead;
    }
}
