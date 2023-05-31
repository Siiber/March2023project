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
            EnemyH.TakeDamage(damage);
            if (EnemyH.health < 1 && pC.vampiricMelee)
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

        hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
