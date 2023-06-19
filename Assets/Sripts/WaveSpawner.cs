using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnInterval;
    public int enemiesPerWave;
    public int maxEnemiesPerWave;
    public float initialwait;
    private bool initialTime = false;

    public float timeBetweenWaves = 5f;
    public float timeSinceLastWave = 0f;
    public ScoreSys points;
    private float score;

    private bool waveCompleted = false;
    private int enemiesDefeated = 0;
    private int enemiesSpawned = 0;
    private float nextSpawnTime = 3f;
    private int scoreMilestone = 300;
    private int lastResetScore = 0;

    private void Start()
    {
        StartCoroutine(StartPeriod());
    }

    private IEnumerator StartPeriod()
    {
        yield return new WaitForSeconds(initialwait);
        initialTime= true;
    }

    void Update()
    {
        if (initialTime)
        {
            score = points.score;
            if (score <= 1000)
            {
                if (waveCompleted & score >= lastResetScore + scoreMilestone)
                {
                    lastResetScore = Mathf.FloorToInt(score / scoreMilestone) * scoreMilestone;
                    enemiesPerWave = 4;
                }
            }

            if (score > 1000 && score<= 2000)
            {
                if (waveCompleted & score >= lastResetScore + scoreMilestone)
                {
                    lastResetScore = Mathf.FloorToInt(score / scoreMilestone) * scoreMilestone;
                    enemiesPerWave = 6;
                }
            }

            else if (score > 2000)
            {
                if (waveCompleted & score >= lastResetScore + scoreMilestone)
                {
                    lastResetScore = Mathf.FloorToInt(score / scoreMilestone) * scoreMilestone;
                    enemiesPerWave = 10;
                }
            }

            waveCompleted = false;

            //first waits the time between waves and then starts spawning enemies until the enemiesPerWave is filled
            if (Time.time > nextSpawnTime && enemiesSpawned < enemiesPerWave)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnInterval;
            }
            //when all enemies in a wave are defeated wave is completed
            if (enemiesDefeated >= enemiesPerWave)
            {
                waveCompleted = true;
            }
            //move to the next wave and 
            if (waveCompleted)
            {
                timeSinceLastWave += Time.deltaTime;
                if (timeSinceLastWave > timeBetweenWaves)
                {
                    enemiesPerWave = Mathf.Min(enemiesPerWave + 1);
                    enemiesSpawned = 0;
                    enemiesDefeated = 0;
                    timeSinceLastWave = 0f;
                    waveCompleted = false;
                }
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
        int healthUp = 50;

        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)] ;
        AttributesManager attributesManager = enemyPrefab.GetComponent<AttributesManager>();
        int baseHealth = 100;

        int scoreIncrement = Mathf.FloorToInt(score / 200f);
        int healthIncrease = scoreIncrement * healthUp;

        int totalHealth = baseHealth + healthIncrease;

        attributesManager.health = totalHealth;

        float x = Random.Range(-11f, 11f);
        float z = Random.Range(6f, 11f);

        Vector3 spawnPosition = new Vector3(x, 0f, z);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        enemiesSpawned++;
    }
}