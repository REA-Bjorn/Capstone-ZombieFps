using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private CustomTimer distractEnemiesTimer;

    // Spawn point collection
    private List<GameObject> spawnPoints = new List<GameObject>();
    // All enemies spawned
    private List<EnemyBase> enemyPool = new List<EnemyBase>();
    [SerializeField] private Transform enemyParent;

    // Goal Trackers
    private int currentEnemyGoal = 0;
    private int currentEnemyGoalStored = 0;

    public float Time => waveCountdownTimer.DurationTime;

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
            GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyParent);
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
        waveCountdownTimer.OnEnd += WaveCountdownEnded;

        distractEnemiesTimer.OnEnd += EnemiesGoBackToNormal;
    }

    private void OnDestroy()
    {
        // Wave Countdown Timer events
        waveCountdownTimer.OnStart -= WaveCountdownStarted;
        waveCountdownTimer.OnEnd -= WaveCountdownEnded;

        distractEnemiesTimer.OnEnd -= EnemiesGoBackToNormal;
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
        // Update Wave
        waveCount++;
        UIManager.Instance.FlashWaveCounter();
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

        // find if current goal is less than the limit, if so use goal
        // otherwise far too many enemies needed so we use our limit
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
        // handles maxing the stats for us
        _enemy.SpawnMe();

        // Gets and stores a random transform
        Transform tr = GetRandomSpawnPoint();
        _enemy.transform.position = tr.position;

        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(_enemy.transform.position, out myNavHit, 0.5f, -1))
        {
            _enemy.transform.position = myNavHit.position;
        }
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
        GameObject tra = spawnPoints[idx];

        // Find active transform if the current one was disabled
        while (!tra.activeInHierarchy)
        {
            idx = Random.Range(0, spawnPoints.Count);
            tra = spawnPoints[idx];
        }

        return tra.transform;
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

    public void DistractEnemies()
    {
        distractEnemiesTimer.StartTimer();
        foreach (EnemyBase enemy in enemyPool)
        {
            enemy.ToggleDistracted();
        }
    }

    private void EnemiesGoBackToNormal()
    {
        foreach (EnemyBase enemy in enemyPool)
        {
            enemy.ToggleDistracted();
        }
    }
}
