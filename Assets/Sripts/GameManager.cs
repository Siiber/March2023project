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

    }

    public void RifleON()
    {
        rifle= true;
        shotgun= false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
