using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChasingEnemy : MonoBehaviour
{
    public Transform target;

    [Header("Stats")]
    public int damage;
    public float enemyspeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);

        transform.Translate(Vector3.forward * enemyspeed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Take target attributes
            PlayerHP EnemyH = other.transform.GetComponent<PlayerHP>();
            //Deal damage
            EnemyH.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
