using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public WaveSpawner wS;

    void Start()
    {
        wS = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
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
        Destroy(gameObject);
    }
}