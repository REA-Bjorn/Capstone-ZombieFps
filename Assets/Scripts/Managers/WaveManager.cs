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
    private List<EnemyBase> enemyPool = new List<EnemyBase>();

    // Goal Trackers
    private int currentEnemyGoal = 0;
    private int currentEnemyGoalStored = 0;

    private int waveCount = 0;
    public string CurrentWave => waveCount.ToString();

    private void Start()
    {
        FindAllSpawnPoints();

        if (spawnPoints.Count == 0)
            return;

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
            // Get Component -- While not good to have a bunch of them,
            // this is only called once at the start of the level
            // so I am determining it to be fine here
            enemyPool.Add(spawnedEnemy.GetComponent<EnemyBase>());
        }
    }

    private void FindAllSpawnPoints()
    {
        // Find all spawn points for enemies
        var points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject point in points)
        {
            spawnPoints.Add(point);
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

    private void SpawnEnemy()
    {
        for (int i = 0; i < enemyPool.Count; ++i)
        {
            // enable the first deactivated enemy
            if (!enemyPool[i].gameObject.activeInHierarchy)
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
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        // Update Total Enemies to kill
        currentEnemyGoalStored += Random.Range(1, 5);
        currentEnemyGoal = currentEnemyGoalStored;

        // Update Wave
        waveCount++;
        UIManager.Instance.UpdateWaveCounter();

        int loopFor = currentEnemyGoal < SpawnLimit ? currentEnemyGoal : SpawnLimit;

        for (int i = 0; i < loopFor; ++i)
        {
            // Spawn the starting amount of enemies
            // - When enemies die, the wave manager decides if it should enable another enemy.
            SpawnEnemy();
            yield return new WaitForSeconds(0.25f);
        }
    }

    void EnableEnemy(EnemyBase _enemy)
    {
        _enemy.gameObject.SetActive(true);
        _enemy.SpawnMe(); // handles maxing the stats for us

        _enemy.transform.parent = GetRandomSpawnPoint().transform;
        _enemy.transform.position = GetRandomSpawnPoint().position;
    }

    public void EnemyKilled(GameObject _enemy)
    {
        --currentEnemyGoal; // subtract enemies

        // The +1 is because we disable the enemy after this function and not before hand
        // Anything above 10 would result in not spawning the last enemy therefore not spawning the next wave.
        if (currentEnemyGoal - TotalCurrentlyUndead() + 1 > 0)
        {
            // we still have enemies to kill
            SpawnEnemy();
        }
        else if (currentEnemyGoal == 0)
        {
            waveCountdownTimer.StartTimer();
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        int idx = Random.Range(0, spawnPoints.Count);
        Transform tra = spawnPoints[idx].transform;

        // Find active transform if the current one was disabled
        while (!tra.gameObject.activeInHierarchy)
        {
            idx = Random.Range(0, spawnPoints.Count);
            tra = spawnPoints[idx].transform;
        }

        return tra;
    }

    private int TotalCurrentlyUndead()
    {
        int currentlyUndead = 0;

        foreach (EnemyBase enemy in enemyPool)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                ++currentlyUndead;
            }
        }

        return currentlyUndead;
    }

    public void KillAllAliveEnemies()
    {
        foreach (EnemyBase enemy in enemyPool)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                enemy.ForceKill();
            }
        }
    }

}
