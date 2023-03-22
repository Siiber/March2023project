using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChasingEnemy : MonoBehaviour
{
    public Transform target;

    public float enemyspeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);

        transform.Translate(Vector3.forward * enemyspeed * Time.deltaTime);

    }
}
