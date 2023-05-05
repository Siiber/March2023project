using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int health;
    private static int maxHealth = 100;
    public Healthbar healthbar;

    private void Start()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
    }

    public void FillHp()
    {
        health = maxHealth;
        healthbar.SetHealth(health);
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
