using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;

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
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
