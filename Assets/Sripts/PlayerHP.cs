using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int health;
    private static int maxHealth = 100;
    public Healthbar healthbar;
    public float iFdur;
    public Renderer[] pModelRender;

    private bool isInvincible = false;
    private float timeOfLastHit = -1f;

    private void Start()
    {

    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible) 
        {
            health -= damage;
            timeOfLastHit= Time.time;
            isInvincible= true;
            healthbar.SetHealth(health);
            StartCoroutine(InviTimer());
            StartCoroutine(InviVisual());
        }
    }

    public void FillHp()
    {
        health = maxHealth;
        healthbar.SetHealth(health);
    }

    void Update()
    {

        if (!isInvincible && Time.time - timeOfLastHit>iFdur)
        {
            isInvincible= false;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator InviTimer()
    {
        yield return new WaitForSeconds(iFdur);
        isInvincible = false;
    }
    private IEnumerator InviVisual()
    {
        float interval = 0.1f;
        while (isInvincible)
        {
            foreach (Renderer renderer in pModelRender)
            {
                renderer.enabled = !renderer.enabled;
            }
            yield return new WaitForSeconds(interval);
        }
        foreach (Renderer renderer in pModelRender)
        {
            renderer.enabled = true;
        }
    }

}
