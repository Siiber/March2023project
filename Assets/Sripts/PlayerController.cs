using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [Range(1f, 15f)]
    public float playerSpeed =1f;

    [Header("Shooting")]
    public Transform gun;
    public GameObject gunAxis;
    public GameObject bullet;
    public float fireRate= 0.3f;
    public bool firing = true;
    public ParticleSystem gunFlame;

    [Header("Game Mode")]
    public bool twinStick = false;

    // Start is called before the first frame update
    void Start()
    {
        if (twinStick)
        {
            gunAxis.GetComponent<ControllerAim>().enabled = true;
            gunAxis.GetComponent<GunScript>().enabled = false;
        }

        else
        {
            gunAxis.GetComponent<ControllerAim>().enabled = false;
            gunAxis.GetComponent<GunScript>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(playerSpeed * Time.deltaTime*hor,0,playerSpeed * Time.deltaTime * ver));

        //Shooting

        if (Input.GetButton("Fire1") && firing)
        {
                Shoot();          
        }
    }

    public void Shoot()
    {
        StartCoroutine(Fire());
    }

    public IEnumerator Fire()
    {
        
        Instantiate(bullet, gun.position, gun.rotation);
        gunFlame.Play();
        firing = false;
        yield return new WaitForSeconds(fireRate);
        firing = true;

    }
}
