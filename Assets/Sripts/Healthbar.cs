using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI Healthtext;
    public TextMeshProUGUI EnergyText;
    public PlayerHP pHP;
    public PlayerController pC;


    void Start()
    {
        pHP = GameObject.Find("Player").GetComponent<PlayerHP>();
        pC = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void Update()
    {
        if (Healthtext != null)
        {
            Healthtext.text = "HP: " + pHP.health;
        }
        if (EnergyText != null)
        {
            EnergyText.text = "ENERGY: " + pC.energy;
        }
        else return;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health) 
    { 
        slider.value = health;
    }
}
