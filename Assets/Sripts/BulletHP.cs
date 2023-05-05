using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHP : MonoBehaviour
{
    public int health;
    void Start()
    {

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
        Destroy(gameObject);
    }
}
