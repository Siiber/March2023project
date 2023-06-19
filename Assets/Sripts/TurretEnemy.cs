using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public Transform target;
    public GameObject[] bullets;
    public float cooldownduration = 2f;
    public bool shooting= true;
    public float coneAngle;
    public float numProjectiles;
    private float waitTimer = 0.5f;
    private bool waitOver = false;
    public AttributesManager aM;
    private bool dead;
    public ScoreSys scoreSys;
    private bool bigBadBullets= false;
    public float scoreThreshold= 100;
    public int projectileCountIncrease = 500;
    private int projectileCountIncReset= 0;


    // Start is called before the first frame update
    void Start()
    {
        scoreSys = GameObject.Find("ScoringSystem").GetComponent<ScoreSys>();
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
        if (scoreSys.score >= scoreThreshold)
        {
            bigBadBullets= true;
        }
        if (scoreSys.score >= projectileCountIncReset + projectileCountIncrease)
        {
            projectileCountIncReset = Mathf.FloorToInt(scoreSys.score / projectileCountIncrease) * projectileCountIncrease;
            numProjectiles += 1;
        }

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
                if (bigBadBullets)
                {
                    int randomIndex = Random.Range(0, bullets.Length);
                    Instantiate(bullets[randomIndex], transform.position, bulletRotation);
                }
                else
                    Instantiate(bullets[0], transform.position, bulletRotation);
            }
            StartCoroutine(Cooldown());
        }
        
    }
}

