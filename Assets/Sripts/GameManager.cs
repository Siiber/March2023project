using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public PlayerController pC;

    public bool shotgun;
    public bool rifle;
    public GameObject weaponCheckmark1;
    public GameObject weaponCheckmark2;

    void Awake()
    {

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ShotgunON()
    {
        shotgun= true;
        rifle = false;
        if (weaponCheckmark1 & weaponCheckmark2 != null)
        {
            weaponCheckmark1.SetActive(false);
            weaponCheckmark2.SetActive(true);
        }
    }

    public void RifleON()
    {
        rifle= true;
        shotgun= false;
        if (weaponCheckmark1 & weaponCheckmark2 != null)
        {
            weaponCheckmark1.SetActive(true);
            weaponCheckmark2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
