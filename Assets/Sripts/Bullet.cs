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

        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {

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
        bulletCollider.enabled = false;
        bulletMesh.enabled = false;
        
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
