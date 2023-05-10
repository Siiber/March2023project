using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnInterval;
    public int enemiesPerWave;
    public int numberOfWaves;
    public int totalEnemies;

    public float timeBetweenWaves = 5f;
    public float timeSinceLastWave = 0f;

    private bool waveCompleted = false;
    private int enemiesDefeated = 0;
    private int currentWave = 0;
    private int enemiesSpawned = 0;
    private float nextSpawnTime = 3f;

    private void Start()
    {
    }

    void Update()
    {
        waveCompleted= false;
        
        if (Time.time >nextSpawnTime && enemiesSpawned <enemiesPerWave)
        {
            SpawnEnemy();
            nextSpawnTime= Time.time + spawnInterval;
        }

        if (enemiesDefeated >= totalEnemies)
        {
            waveCompleted = true;

            if (currentWave < numberOfWaves)
            {
                currentWave++;
                enemiesSpawned = 0;
                enemiesDefeated = 0;
                totalEnemies = enemiesPerWave;
                waveCompleted = false;
            }
        }

        if (waveCompleted)
        {
            timeSinceLastWave += Time.deltaTime;
            if (timeSinceLastWave > timeBetweenWaves)
            {
                enemiesPerWave += 2;
                currentWave++;
                enemiesSpawned = 0;
                enemiesDefeated = 0;
                totalEnemies = enemiesPerWave;
                timeSinceLastWave = 0f;
                waveCompleted = false;
            }
        }
    }

    public void OnEnemyDeath()
    {
        enemiesDefeated++;

        if (enemiesDefeated >= enemiesPerWave)
        {
            print("All enemies defeated") ;
            waveCompleted = true;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];

        float x = Random.Range(-12f, 12f);
        float z = Random.Range(7f, 10f);

        Vector3 spawnPosition = new Vector3(x, 0f, z);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        enemiesSpawned++;
    }
}