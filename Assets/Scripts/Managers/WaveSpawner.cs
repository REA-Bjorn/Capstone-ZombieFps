using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [SerializeField] private List<GameObject> enemyPool;

    [SerializeField] int StartingLimit = 10;

    [SerializeField] GameObject enemyPre;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public int count;
        public float rate;
    }

    private SpawnState state = SpawnState.COUNTING;
    private int nextWave = 0;

    public Wave[] waves;
    public GameObject[] spawnPoints;

    public float timeBetweenWaves = 5f;

    private float waveCountdown;
    private float searchCountdown = 1f;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points");
        }

        for (int i = 0; i < StartingLimit; i++)
        {
            SpawnEnemy(enemyPre);
        }

        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(StartWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All Waves Complete! Looping...");
        }
        else
        {
            nextWave++;
        }

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator StartWave(Wave _wave)
    {
        Debug.Log("Spawing Wave: " + _wave.name);

        state = SpawnState.SPAWNING;

        if (enemyPool.Count < _wave.count)
        {
            int loopsize = _wave.count;

            for (int i = 0; i < loopsize; i++)
            {
                SpawnEnemy(enemyPre);
            }

        }

        for (int i = 0; i < _wave.count; i++)
        {
            EnableEnemy(enemyPool[i]);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void EnableEnemy(GameObject _enemy)
    {
        _enemy.SetActive(true);
        _enemy.GetComponent<EnemyBase>().MaxStats();
    }


    void SpawnEnemy(GameObject _enemy)
    {
        Debug.Log("Spawing Enemy: " + _enemy.name);

        GameObject _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject _go = Instantiate(_enemy, _sp.transform);
        _go.SetActive(false);

        enemyPool.Add(_go);
    }

}
