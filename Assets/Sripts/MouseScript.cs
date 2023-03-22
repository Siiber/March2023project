using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public float Mdelay = 0.1f;

    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, Mathf.Infinity,layerMask))
        {
            transform.position = Vector3.Lerp(transform.position,hit.point,Mdelay);

            /*if (Input.GetButtonDown("Fire1") && hit.transform.CompareTag("Enemy"))
            {
                Destroy(hit.transform.gameObject);
            } */

            
        }

        //Debug.DrawRay(Camera.main.transform.position, Vector3.forward, Color.black, Mathf.Infinity);
    }
}
