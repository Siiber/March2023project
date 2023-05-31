using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerEnergy : MonoBehaviour
{
    public Slider slider;
    public PlayerController pC;
    public int energy;

    private void Start()
    {
        energy = pC.energy;
    }

    void Update()
    {
        energy =   pC.energy;
        slider.value = energy;
    }
}
