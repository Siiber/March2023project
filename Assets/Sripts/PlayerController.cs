using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [Range(1f, 15f)]
    public float playerSpeed =1f;
    private bool isDashing = false;
    public Rigidbody playerRigidbody;

    [Header("Shooting")]
    public Transform gun;
    public GameObject gunAxis;
    public GameObject bullet;
    public Bullet bulletstats;
    public float fireRate= 0.3f;
    public int riflebulletdmg;
    public bool firing = true;
    public ParticleSystem gunFlame;
    public GameObject shotgunBullet;
    public Bullet shotgunBulletStats;
    public float shotgunPellets;
    public float shotgunConeAngle;
    public float shotgunFireRate = 0.6f;
    public int shotgunbulletdmg;

    [Header("Melee")]
    public PlayerHP pHP;
    public int energy;
    public TrailRenderer[] trail;
    public bool melee = true;
    public Animator meleeAnimator;
    public SwordAttr swordAttributes;
    public PlayerEnergy energymeter;
    
    [Header("Game Mode")]
    public bool twinStick = false;
    public GameManager gm;
    public bool rifle;
    public bool shotgun;

    [Header("Perks")]
    private ButtonManager bM;
    public bool sprongedBullets;
    public bool vampiricMelee;

    // Start is called before the first frame update
    void Start()
    {

        gm= GameObject.Find("GM").GetComponent<GameManager>();
        pHP = GetComponent<PlayerHP>();
        bM = FindObjectOfType<ButtonManager>();
        rifle = gm.rifle;
        shotgun= gm.shotgun;

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

        DisableTrailRenderer();
        energy = 50;
    }

    // Update is called once per frame
    void Update()
    {
        energy = Mathf.Clamp(energy, 0, 100);

        //Movement
        if (!isDashing)
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(playerSpeed * Time.deltaTime * hor, 0, playerSpeed * Time.deltaTime * ver));
        }

        //Shooting

        if (Input.GetButton("Fire1") && firing)
        {
            Shoot();          
        }

        if (Input.GetButton("Fire2") && melee)
        {
            if (energy > 25)
            {
                energy -= 25;
                Melee();
            }
            else
                return;
        }
    }

    public void Shoot()
    {
        if (rifle)
        {
            StartCoroutine(Fire());
        }
        if (shotgun)
        {
            StartCoroutine(ShotgunFire());
        }
    }

    public void Melee()
    {
        if (rifle)
        {
            StartCoroutine(Sword());
        }

        if (shotgun && !isDashing)
        {
            StartCoroutine(ShotgunDash());
        }
    }

    //Basic rifle firing
    public IEnumerator Fire()
    {
        if (!sprongedBullets)
        {
            Instantiate(bullet, gun.position, gun.rotation);
        }
        if (sprongedBullets)
        {
            Quaternion originalRotation = gun.rotation;

            for (int i = 0; i < 3; i++)
            {
                // Calculate the forked direction
                Quaternion forkRotation = Quaternion.Euler(0, -10 + (10 * i), 0);
                Quaternion newRotation = originalRotation * forkRotation;

                // Instantiate the bullet with the calculated rotation
                Instantiate(bullet, gun.position, newRotation);
            }
        }

        gunFlame.Play();
        firing = false;
        yield return new WaitForSeconds(fireRate);
        firing = true; 
    }
    //Basic rifle melee
    public IEnumerator Sword()
    {
        swordAttributes.hitbox.enabled = true;
        
        meleeAnimator.SetTrigger("Swing");
        melee = false;
        EnableTrailRenderer();

        yield return new WaitForSeconds(0.5f);

        melee= true;
        DisableTrailRenderer();
        swordAttributes.hitbox.enabled = false;
    }
    //Basic shotgun fire
    public IEnumerator ShotgunFire()
    {
        Vector3 bulletDirection = gun.position - transform.position;
        Quaternion baseRotation = Quaternion.LookRotation(bulletDirection);
        if (!sprongedBullets)
        {
            for (int i = 0; i < shotgunPellets; i++)
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-shotgunConeAngle, shotgunConeAngle), 0);
                Quaternion bulletRotation = baseRotation * randomRotation;
                Instantiate(shotgunBullet, gun.position, bulletRotation);
            }
        }
        if (sprongedBullets)
        {
            for (int i = 0; i < shotgunPellets + 4f; i++)
            {
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-shotgunConeAngle, shotgunConeAngle), 0);
                Quaternion bulletRotation = baseRotation * randomRotation;
                Instantiate(shotgunBullet, gun.position, bulletRotation);
            }
        }

        gunFlame.Play();
        firing = false;
        yield return new WaitForSeconds(shotgunFireRate);
        firing = true;
    }
    //Basic shotgun dash
    public IEnumerator ShotgunDash()
    {
        isDashing = true;
        //dash
        Quaternion initialRotation = transform.rotation;
        Vector3 dashDirection = gun.forward;
        playerRigidbody.isKinematic = true;
        float dashDistance = 20f;
        float dashDuration = 0.5f;
        Vector3 dashVelocity = dashDirection * (dashDistance/dashDuration);
        GetComponent<Rigidbody>().velocity = dashVelocity;
        transform.rotation = initialRotation;
        playerRigidbody.isKinematic = false;
        isDashing = false;

        //Melee part
        swordAttributes.hitbox.enabled = true;

        meleeAnimator.SetTrigger("ShotgunSwing");
        melee = false;
        EnableTrailRenderer();

        yield return new WaitForSeconds(1f);

        melee = true;
        DisableTrailRenderer();
        swordAttributes.hitbox.enabled = false;
    }

    //Playmode swap ==============================================
    public void PlaymodeChange()
    {
        
        if (twinStick)
        {
            twinStick = false;
        }

        if (!twinStick)
        {
            twinStick= true;
        }
    }

    //toggles for all the trail renderers
    private void EnableTrailRenderer()
    {
        foreach (TrailRenderer renderer in trail)
        {
            renderer.enabled = true;
        }
    }
    private void DisableTrailRenderer()
    {
        foreach (TrailRenderer renderer in trail)
        {
            renderer.enabled = false;
        }
    }

    //PERKS====================================================

    public void ActivateSprongedBullets()
    {
        if (sprongedBullets)
        {
            riflebulletdmg += 10;
            shotgunbulletdmg += 10;
        }
        Debug.Log("button sprong");
        sprongedBullets = true;
    }

    public void ActivateVampiricMelee()
    {
        if (vampiricMelee)
        {
            pHP.RegenAmount += 5;
        }
        Debug.Log("button vamp");
        vampiricMelee = true;
    }

    public void ActivateSpeedster()
    {
        Debug.Log("button speed");
        playerSpeed += 0.5f;
    }

    public void ActivateFasterFire()
    {
        Debug.Log("button fastfire");
        fireRate -= 0.05f;
    }

    //PERKS END HERE==========================================
}


