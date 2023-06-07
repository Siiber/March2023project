using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject[] buttonVariations;
    private PlayerController playerController;
    public Transform[] perkPos;


    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {

    }

    public void SpawnRandomButtons()
    {

        Debug.Log("SpawnRandomButtons called");

        // Clear existing buttons if any
        ClearButtons();

        // Generate random positions to spawn buttons
        int[] randomIndices = GenerateRandomIndices(3);

        // Instantiate the random button prefabs at spawn positions
        for (int i = 0; i < perkPos.Length; i++)
        {
            bool vampiricMelee = playerController.vampiricMelee;
            bool sprongedBullets = playerController.sprongedBullets;

            int randomIndex = randomIndices[i];
            GameObject buttonPrefab = buttonVariations[randomIndex];

            if (vampiricMelee && i == 0) 
            {
                buttonPrefab = buttonVariations[0];
            }

            if (sprongedBullets && i == 0)
            {
                buttonPrefab = buttonVariations[1];
            }

            else
            {
                buttonPrefab = buttonVariations[randomIndex];
            }

            Vector3 spawnPosition = perkPos[i].position;

            Instantiate(buttonPrefab, spawnPosition, Quaternion.identity, transform);
        }
    }

    public void ClearButtons()
    {
        // Destroy existing buttons if any
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private int[] GenerateRandomIndices(int count)
    {
        int[] indices = new int[count]; 
        for(int i = 0; i< count; i++)
        {
            indices[i] = Random.Range(0, buttonVariations.Length);
        }
        return indices;
    }

}

