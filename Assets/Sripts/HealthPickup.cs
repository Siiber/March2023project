using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public ParticleSystem pickup;
    public Animator open;
    public int damage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("asd");
            //Take target attributes
            PlayerHP EnemyH = other.transform.GetComponent<PlayerHP>();
            EnemyH.FillHp();
            StartCoroutine(Dissappear());
        }
    }

    public IEnumerator Dissappear ()
    {
        open.SetTrigger("Open");
        yield return new WaitForSeconds(0.1f);
        pickup.Play();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
