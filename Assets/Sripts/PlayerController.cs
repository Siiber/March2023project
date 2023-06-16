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
    public ParticleSystem gunFlame;
    public bool firing = true;

    [Header("Rifle")]
    public Transform gun;
    public GameObject gunAxis;
    public GameObject bullet;
    public Bullet bulletstats;
    public float fireRate= 0.3f;
    public int riflebulletdmg;
    
    [Header("Shotgun")]
    public GameObject shotgunBullet;
    public Bullet shotgunBulletStats;
    public float shotgunPellets;
    public float shotgunConeAngle;
    public float shotgunFireRate = 0.6f;
    public int shotgunbulletdmg;

    [Header("Sniper")]
    public GameObject sniperBullet;
    public Bullet sniperBulletStats;
    public Bullet sprongSniperBullets;
    public float sniperFireRate = 0.6f;
    public int sniperBulletdmg;
    public GameObject sniperMeleeProjectile;
    public int sniperWaveDMG;

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
    public bool sniper;

    [Header("Perks")]
    private ButtonManager bM;
    public bool sprongedBullets;
    public int sprongCount = 0;
    public bool superSprong= false;
    public bool vampiricMelee;
    public int vampiricMeleeCount;
    public bool hyperVamp = false;
    public bool easymode;

    // Start is called before the first frame update
    void Start()
    {
        gm= GameObject.Find("GM").GetComponent<GameManager>();
        easymode = gm.easymode;
        pHP = GetComponent<PlayerHP>();
        bM = FindObjectOfType<ButtonManager>();
        rifle = gm.rifle;
        shotgun= gm.shotgun;
        sniper= gm.sniper;
        twinStick = gm.twinstick;

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

        if (easymode)
        {
            sprongedBullets= true;
            sprongCount= 3;
            vampiricMelee= true;
            vampiricMeleeCount= 3;
            playerSpeed += 3;
        }

        DisableTrailRenderer();
        energy = 50;
    }

    // Update is called once per frame
    void Update()
    {
        energy = Mathf.Clamp(energy, 0, 100);
        if (vampiricMeleeCount >= 3)
        {
            hyperVamp= true;
        }

        if (sprongCount >= 3)
        {
            superSprong = true;
        }

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
            if (!sniper && energy >= 25)
            {
                energy -= 25;
                Melee();
            }

            if (sniper && energy >= 50)
            {
                energy -= 50;
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
        if (sniper)
        {
            StartCoroutine(SniperFire());
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

        if (sniper)
        {
            StartCoroutine(SniperSword());
        }
    }

    //===============================================================================Basic rifle firing
    public IEnumerator Fire()
    {
        if (!sprongedBullets)
        {
            Instantiate(bullet, gun.position, gun.rotation);
        }
        if (sprongedBullets)
        {
            Quaternion originalRotation = gun.rotation;

            for (int i = 0; i < 5; i++)
            {
                // Calculate the forked direction
                Quaternion forkRotation = Quaternion.Euler(0, -10 + (5 * i), 0);
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
    //=================================================================================================Basic shotgun fire
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
            for (int i = 0; i < shotgunPellets + 5f; i++)
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
        float dashDistance = 20f;
        float dashDuration = 0.5f;
        Vector3 dashVelocity = dashDirection * (dashDistance/dashDuration);
        GetComponent<Rigidbody>().velocity = dashVelocity;
        transform.rotation = initialRotation;
        isDashing = false;


        //Melee part
        swordAttributes.hitbox.enabled = true;

        meleeAnimator.SetTrigger("ShotgunSwing");
        melee = false;
        EnableTrailRenderer();

        yield return new WaitForSeconds(dashDuration);
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        
        DisableTrailRenderer();

        swordAttributes.hitbox.enabled = false;

        yield return new WaitForSeconds(1f);
        melee = true;

    }
    //====================================================================================SNIPER FIRE HERE ===============================================
    public IEnumerator SniperFire()
    {
        if (!sprongedBullets)
        {
            Instantiate(sniperBullet, gun.position, gun.rotation);
        }
        if (sprongedBullets)
        {
            Instantiate(sprongSniperBullets, gun.position, gun.rotation);
        }

        gunFlame.Play();
        firing = false;
        yield return new WaitForSeconds(sniperFireRate);
        firing = true;
    }

    public IEnumerator SniperSword()
    {
        swordAttributes.hitbox.enabled = true;

        meleeAnimator.SetTrigger("SniperSwing");
        melee = false;
        EnableTrailRenderer();
        Instantiate(sniperMeleeProjectile, gun.position, gun.rotation);
        yield return new WaitForSeconds(0.5f);

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
        sprongCount++;
        if (sprongedBullets)
        {
            riflebulletdmg += 10;
            shotgunbulletdmg += 10;
            sniperBulletdmg += 50;
        }
        Debug.Log("button sprong");
        sprongedBullets = true;
    }

    public void ActivateVampiricMelee()
    {
        vampiricMeleeCount++;
        if (vampiricMelee)
        {
            swordAttributes.damage += 100;
            sniperWaveDMG += 100;
            pHP.RegenAmount += 2;
        }
        Debug.Log("button vamp");
        vampiricMelee = true;
    }

    public void ActivateSpeedster()
    {
        Debug.Log("button speed");
        playerSpeed += 1f;
    }

    public void ActivateFasterFire()
    {
        Debug.Log("button fastfire");
        if (rifle)
            fireRate -= 0.04f;
        if (shotgun)
            shotgunFireRate -= 0.1f;
        if(sniper)
            sniperFireRate -= 0.2f;
    }

    //PERKS END HERE==========================================
}


