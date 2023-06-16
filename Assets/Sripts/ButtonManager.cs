using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{
    public GameObject[] buttonVariations;
    private PlayerController playerController;
    public Transform[] perkPos;
    public EventSysScript eventSys;

    private Selectable[] selectables;

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
            int randomIndex = randomIndices[i];
            GameObject buttonPrefab = buttonVariations[randomIndex];

            buttonPrefab = buttonVariations[randomIndex];

            Vector3 spawnPosition = perkPos[i].position;
            Time.timeScale = 0;
            GameObject button = Instantiate(buttonPrefab, spawnPosition, Quaternion.identity, transform);

            //set the first button selected
            if (i == 0)
            {
                EventSystem.current.SetSelectedGameObject(button);
            }

            // Disable interactability of other selectable objects
        }
    }

    public void ClearButtons()
    {
        Time.timeScale = 1f;
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

