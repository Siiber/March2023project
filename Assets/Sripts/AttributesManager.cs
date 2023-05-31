using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int points;
    public WaveSpawner wS;
    public ScoreSys sS;

    void Start()
    {
        wS = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        sS = GameObject.FindObjectOfType<ScoreSys>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Update()
    {
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        wS.OnEnemyDeath();
        sS.AddScore(points);
        Destroy(gameObject);
    }
}
