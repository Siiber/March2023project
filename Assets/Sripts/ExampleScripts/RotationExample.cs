using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class RotationExample : MonoBehaviour
{
    public float rotatespeed;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = new Quaternion(0f,0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(1f, 2f, 3f);
        float hor = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward*speed*Time.deltaTime);
        transform.Rotate(Vector3.up,hor * rotatespeed*Time.deltaTime);
    }
}
