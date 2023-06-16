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
    public bool easymode = false;
    public bool twinstick = false;

    public bool shotgun;
    public bool rifle;
    public bool sniper;

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

    private void Start()
    {
    }

        public void ShotgunON()
    {
        shotgun= true;
        rifle = false;
        sniper = false;

    }

    public void RifleON()
    {
        rifle= true;
        shotgun= false;
        sniper= false;
    }

    public void SniperON()
    {
        sniper= true;
        shotgun= false;
        rifle= false;
    }
    public void Easymode()
    {
        if (!easymode)
            easymode = true;
        else if (easymode)
            easymode = false;
    }

    public void Twinstick()
    {
        if (!twinstick)
            twinstick = true;
        else if(twinstick)
            twinstick = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
