using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public float lifetime;
    public ScoreSys scoreSys;

    [Header("Stats")]
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        scoreSys = GameObject.Find("ScoringSystem").GetComponent<ScoreSys>();
        bulletSpeed = scoreSys.bulletspeed;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

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
