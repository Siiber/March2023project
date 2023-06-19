using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerHP : MonoBehaviour
{
    public int health;
    private static int maxHealth = 100;
    public Healthbar healthbar;
    public float iFdur;
    public Renderer[] pModelRender;
    public Renderer[] gunMRenderer;
    public int RegenAmount = 25;
    public bool isDead= false;
    public PlayerController pc;
    public ControllerAim ca;
    public GameObject player;
    public ParticleSystem explosion;
    public AudioManager audioManager;
    public Animator cam;
    public EventSysScript eSys;

    public int sprongCount;
    public int vampiricCount;
    public int speedCount;
    public int rateCount;

    public TextMeshProUGUI sprongText;
    public TextMeshProUGUI vampiricText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI rateText;



    private bool isInvincible = false;
    private float timeOfLastHit = -1f;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cam = GameObject.Find("Main Camera").GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible) 
        {
            cam.SetTrigger("Hit");
            audioManager.Play("HitPlayer");
            health -= damage;
            timeOfLastHit= Time.time;
            isInvincible= true;
            healthbar.SetHealth(health);
            StartCoroutine(Hitstop());
            StartCoroutine(InviTimer());
            if (health >= 1) 
            {
                StartCoroutine(InviVisual());
            }
        }
    }

    public void FillHp()
    {
        audioManager.Play("Heal");
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

        sprongCount = pc.sprongCount;
        vampiricCount = pc.vampiricMeleeCount;
        speedCount = pc.speedCount;
        rateCount = pc.rateCount;

        sprongText.text = sprongCount.ToString();
        vampiricText.text = vampiricCount.ToString();
        speedText.text = speedCount.ToString();
        rateText.text = rateCount.ToString();


        if (!isInvincible && Time.time - timeOfLastHit>iFdur)
        {
            isInvincible= false;
        }
        if (health <= 0)
        {
            if (!isDead)
            {
                audioManager.Play("Explosion");
                Instantiate(explosion, transform.position, transform.rotation);
            }
            isDead = true;
            pc.enabled= false;
            ca.enabled= false;
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            SphereCollider collider2 = GetComponent<SphereCollider>();
            if (collider!= null)
            {
                collider.enabled = false;
                collider2.enabled= false;
            }
            foreach(Renderer renderer in pModelRender) 
            {
                renderer.enabled = false;
            }            
            foreach(Renderer renderer in gunMRenderer) 
            {
                renderer.enabled = false;
            }
            
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
    public IEnumerator Hitstop()
    {
        print("hitstop");
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }
}
