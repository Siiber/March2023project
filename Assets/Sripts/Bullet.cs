using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerController controller;
    public float bulletSpeed = 20f;
    public ParticleSystem hitEffect;
    public SphereCollider bulletCollider;
    public MeshRenderer bulletMesh;
    public Transform target;
    public bool twinstick;

    [Header("Stats")]
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();

        if (controller.rifle)
        {
            damage = controller.riflebulletdmg;
        }

        if (controller.shotgun) 
        {
            damage = controller.shotgunbulletdmg;
        }

        if (controller.sniper)
        {
            damage = controller.sniperBulletdmg; 
        }
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        twinstick = controller.twinStick;
        if (controller.shotgun && controller.superSprong)
        {
            transform.localScale += new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
            if (!twinstick)
            {
                target = GameObject.Find("MousePointer").GetComponent<Transform>();
                transform.LookAt(target);
            }
                
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        }
        else
        transform.Translate(Vector3.forward*bulletSpeed* Time.deltaTime);
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
            EnemyH.TakeDamage(damage);
            StartCoroutine (Hit());
        }
    }
    public IEnumerator Hit()
    {
        transform.Translate(Vector3.forward * 0f);
        hitEffect.Play();
        //SNIPER UNIQUE EFFECTS =========================================================
        if (controller.sniper && controller.sprongedBullets)
        {
            Quaternion originalRotation = transform.rotation;
            if (!controller.superSprong)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Calculate the forked direction
                    Quaternion forkRotation = Quaternion.Euler(0, -90 + (90 * i), 0);
                    Quaternion newRotation = originalRotation * forkRotation;

                    // Instantiate the bullet with the calculated rotation
                    Instantiate(controller.sniperBullet, transform.position, newRotation);
                }
            }
            if (controller.superSprong)
            {
                for (int i = 0; i < 5; i++)
                {
                    // Calculate the forked direction
                    Quaternion forkRotation = Quaternion.Euler(0, -90 + (45 * i), 0);
                    Quaternion newRotation = originalRotation * forkRotation;

                    // Instantiate the bullet with the calculated rotation
                    Instantiate(controller.sniperBullet, transform.position, newRotation);
                }
            }
        }
        //SNIPER UNIQUE EFFECTS END HERE =================================================
        if (!controller.sniper)
        bulletCollider.enabled = false;
        bulletMesh.enabled = false;
        

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
