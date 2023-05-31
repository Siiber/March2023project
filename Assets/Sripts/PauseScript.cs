using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool paused= false;
    public GameObject pauseMenu;
    public Animator menuanim;
        

    void Start()
    {
        if (pauseMenu == null) return;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (pauseMenu == null) return;

        if (Input.GetButtonDown("Cancel"))
        {
            if (paused==true)
            {
                paused= false;
            }

            else if (!paused)
            {
                paused = true;
            }
        }

        if (paused == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

        }
        else if(!paused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

}
