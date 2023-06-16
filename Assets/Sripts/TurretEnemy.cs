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
    private float waitTimer = 0.5f;
    private bool waitOver = false;
    public AttributesManager aM;
    private bool dead;


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

    public IEnumerator InitialWaitTimer()
    {
        yield return new WaitForSeconds(waitTimer);
        waitOver= true;
    }

    // Update is called once per frame
    void Update()
    {
        dead = aM.dead;

        if (!waitOver) {
            StartCoroutine(InitialWaitTimer());
        }

        if (target== null) { return; }
        transform.LookAt(target);

        if (waitOver && !dead)
        {
            if (!shooting)
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
}

