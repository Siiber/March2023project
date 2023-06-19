using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWaveAttr : MonoBehaviour
{
    public PlayerController controller;
    public float bulletSpeed = 14f;
    public GameObject swishEffect;
    public BoxCollider Collider;
    public PlayerHP pHP;
    public AudioManager audioManager;

    [Header("Stats")]
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
        pHP = GameObject.Find("Player").GetComponent<PlayerHP>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        damage = controller.sniperWaveDMG;

        Destroy(gameObject, 9f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

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
            if (EnemyH.meleeHealth < 1 && controller.vampiricMelee)
            {
                pHP.Regen();
            }
            audioManager.Play("Melee_sound");
            Instantiate(swishEffect, transform.position, transform.rotation);
        }
    }
}
