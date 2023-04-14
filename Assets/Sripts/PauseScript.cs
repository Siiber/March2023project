using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool paused= false;
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (paused==true)
            {
                paused= false;
            }

            else if (!paused)
            {
                paused= true;
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
