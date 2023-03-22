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

    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.LookAt(target);

        if(shooting == false)
            {
            return;
            }

        if(shooting ==true)
        {
            Instantiate(bullet,transform.position,transform.rotation);
            StartCoroutine(Cooldown());
        }


    }
}
