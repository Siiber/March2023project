using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerHP : MonoBehaviour
{
    public int health;
    private static int maxHealth = 100;
    public Healthbar healthbar;
    public float iFdur;
    public Renderer[] pModelRender;
    public int RegenAmount = 25;

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

    public void Regen()
    {
        health += RegenAmount;
        healthbar.SetHealth(health);
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, 100);

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
