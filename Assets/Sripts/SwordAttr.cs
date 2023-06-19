using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SwordAttr : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public BoxCollider hitbox;
    public PlayerController pC;
    public PlayerHP pHP;
    public AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile1"))
        {
            //Take target attributes
            BulletHP EnemyH = other.transform.GetComponent<BulletHP>();
            //Deal damage
            EnemyH.TakeDamage(damage);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            AttributesManager EnemyH = other.transform.GetComponent<AttributesManager>();
            EnemyH.TakeMeleeDamage(damage);
            StartCoroutine(Hitstop());
            if (EnemyH.meleeHealth < 1 && pC.vampiricMelee)
            {
                pHP.Regen();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pC = GameObject.Find("Player").GetComponent<PlayerController>();
        pHP = GameObject.Find("Player").GetComponent<PlayerHP>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        hitbox.enabled = false;
    }

    public IEnumerator Hitstop()
    {
        print("hitstop");
        audioManager.Play("Melee_sound");
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
