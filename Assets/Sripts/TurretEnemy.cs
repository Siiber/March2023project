using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public Transform target;
    public GameObject bullet;
    public float cooldownduration = 2f;
    public bool shooting= true;
    public float coneAngle;
    public float numProjectiles;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    public IEnumerator Cooldown()
    {
        shooting = false;
        yield return new WaitForSeconds(cooldownduration);
        shooting= true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target== null) { return; }

        transform.LookAt(target);

        if(shooting == false)
            {
            return;
            }

        Vector3 bulletDirection = target.position - transform.position;
        Quaternion baseRotation = Quaternion.LookRotation(bulletDirection);

        for (int i = 0; i < numProjectiles; i++)
        {
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-coneAngle, coneAngle), 0);
            Quaternion bulletRotation = baseRotation * randomRotation;

            Instantiate(bullet, transform.position, bulletRotation);
        }
        StartCoroutine(Cooldown());
        
    }
}

