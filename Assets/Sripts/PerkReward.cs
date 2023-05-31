using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkReward : MonoBehaviour
{
    public ButtonManager perks;

    private void Start()
    { 
        perks = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            perks.SpawnRandomButtons();
            Destroy(gameObject);
        }
    }
}
