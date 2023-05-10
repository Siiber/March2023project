using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int points;
    public WaveSpawner wS;
    public GameManager gM;

    void Start()
    {
        wS = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        gM = GameObject.FindObjectOfType<GameManager>();
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
        gM.AddScore(points);
        Destroy(gameObject);
    }
}
