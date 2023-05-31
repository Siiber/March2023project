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

    private void Awake()
    {
        button = GetComponent<Button>();
        pC = GameObject.Find("Player").GetComponent<PlayerController>();
        bM = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
        button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        bM.ClearButtons();
    }

    public void FasterFire()
        {
             pC.ActivateFasterFire();
        }
    public void Speedster()
        {
             pC.ActivateSpeedster();
        }

    public void SprongedBullets()
        {
            pC.ActivateSprongedBullets();
        }

    public void VampiricMelee()
        {
            pC.ActivateVampiricMelee();
        }


}
