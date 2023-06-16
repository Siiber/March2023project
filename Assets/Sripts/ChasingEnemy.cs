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
    public AttributesManager attributes;
    private float waitTimer = 0.5f;
    private bool waitOver = false;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        dead = attributes.dead;
        if (!waitOver)
        {
            StartCoroutine(InitialWaitTimer());
        }
        transform.LookAt(target);

        if (waitOver)
        {
            transform.Translate(Vector3.forward * enemyspeed * Time.deltaTime);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!dead)
            {
                //Take target attributes
                PlayerHP EnemyH = other.transform.GetComponent<PlayerHP>();
                //Deal damage
                EnemyH.TakeDamage(damage);
                attributes.OnDeath();
            }
        }
    }
    public IEnumerator InitialWaitTimer()
    {
        yield return new WaitForSeconds(waitTimer);
        waitOver = true;
    }
}
