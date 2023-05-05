using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;

    [Header("Stats")]
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
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
            //Take target attributes
            AttributesManager EnemyH = other.transform.GetComponent<AttributesManager>();
            //Deal damage
            EnemyH.TakeDamage(damage);
            //Destroy the gameObject
            Destroy(gameObject);
        }
    }
}
