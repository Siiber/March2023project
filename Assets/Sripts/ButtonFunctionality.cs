using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ButtonFunctionality : MonoBehaviour
{
    private Button button;
    public PlayerController pC;
    public ButtonManager bM;
    public GameManager gameManager;
    public Animator buttonswap;
    public Sprite onButton;
    public Sprite offButton;
    public EventSysScript eventSys;



    private void Awake()
    {
        if (!onButton == null)
        {
            button.image.sprite = onButton;
        }
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        // Find the PlayerController if it exists in the scene
        pC = GameObject.FindObjectOfType<PlayerController>();

        // Find the ButtonManager if it exists in the scene
        bM = GameObject.FindObjectOfType<ButtonManager>();

        gameManager= GameObject.FindObjectOfType<GameManager>();

    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (bM!= null)
        bM.ClearButtons();
    }

    public void Twinstick()
    {
        gameManager.Twinstick();
    }

    public void FasterFire()
        {
            if (pC != null)
            pC.ActivateFasterFire();
        }
    public void Speedster()
        {
            if (pC != null)
            pC.ActivateSpeedster();
        }

    public void SprongedBullets()
        {
        if (pC != null)
            pC.ActivateSprongedBullets();
        }

    public void VampiricMelee()
        {
        if (pC != null)
            pC.ActivateVampiricMelee();
        }

    public void Rifle()
    {
        gameManager.RifleON();
    }

    public void Shotgun()
    {
        gameManager.ShotgunON();
    }

    public void Sniper()
    {
        gameManager.SniperON();
    }

    public void Easymode()
    {
        gameManager.Easymode();
    }

    public void Swapbutton()
    {
        if (button.image.sprite == onButton)
            button.image.sprite = offButton;

        else
            button.image.sprite = onButton;
    }
}
