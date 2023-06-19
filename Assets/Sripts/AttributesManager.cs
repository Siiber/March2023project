using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int meleeHealth;
    public int points;
    public WaveSpawner wS;
    public ScoreSys sS;
    public ParticleSystem explosion;
    public Renderer[] mRenderer;
    public AudioManager audioManager;
    public bool dead =false;

    void Start()
    {
        wS = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        sS = GameObject.FindObjectOfType<ScoreSys>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        meleeHealth = health;
    }

    public void TakeDamage(int damage)
    {
        audioManager.Play("HItEnemy");
        health -= damage;
    }

    public void TakeMeleeDamage(int damage)
    {
        meleeHealth -= damage;
    }

    void Update()
    {
        if (health <= 0 && !dead)
        {
            dead = true;
            audioManager.Play("Explosion_enemy");
            Instantiate(explosion, transform.position, transform.rotation);
            OnDeath();
        }
        if (meleeHealth <= 0 && !dead)
        {
            dead = true;
            audioManager.Play("Explosion_enemy");
            Instantiate(explosion, transform.position, transform.rotation);
            OnMeleeDeath();
        }
    }
    public IEnumerator Death()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        foreach (Renderer renderer in mRenderer)
        {
            renderer.enabled = false;
        }
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }

    public void OnDeath()
    {
        wS.OnEnemyDeath();
        sS.AddScore(points);
        StartCoroutine(Death());
    }
    public void OnMeleeDeath()
    {
        wS.OnEnemyDeath();
        sS.MeleeScoreAdd(points);
        StartCoroutine(Death());
    }
}
