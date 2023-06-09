using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    public PlayerHP playerHP;
    public TextMeshProUGUI gameoverText;
    public EventSysScript eSys;
    private bool hasPlayerDied = false;

    void Start()
    {
        if (gameoverText != null) 
        {
             gameoverText.gameObject.SetActive(false);
        }
        if (pauseMenu == null) return;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (pauseMenu == null) return;

        if (playerHP != null)
        {
            if (playerHP.isDead && !hasPlayerDied)
            {
                hasPlayerDied= true;
                eSys.PauseFirst();
                gameoverText.gameObject.SetActive(true);
                pauseMenu.SetActive(true);
            }
        }

        if (!paused)
        {
            
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (paused == true)
            {
                paused = false;
                PauseOFF();
            }

            else if (!paused)
            {
                eSys.PauseFirst();
                paused = true;
                PauseON();
            }
        }
    }

    public void PauseON()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseOFF()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

}

