using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreSys : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public int score;
    public PlayerController pC;
    public Transform rewardPos;
    public GameObject medicrate;
    public Transform medicrateSpawnpoint;
    private bool startperk = false;

    public GameObject reward;

    private int scoreMilestone = 200;
    private int lastResetScore = 0;

    private int mediScoreMilestone = 500;
    private int lastResetScoreMedicrate = 0;

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score:" + score;
        pC.energy += 10;
    }

    public void MeleeScoreAdd(int points)
    {
        score += points;
        scoreText.text = "Score:" + score;
        if (pC.hyperVamp)
        pC.energy += 10;
        else 
        pC.energy += 3;
    }

    private void Start()
    {
        reward.SetActive(true);
    }

    void Update()
    {
        if (score > 30 && !startperk)
        {
            Instantiate(reward, rewardPos);
            Instantiate(medicrate, medicrateSpawnpoint);

            startperk = true;
        }

        if (score >= lastResetScore + scoreMilestone)
        {
            lastResetScore = Mathf.FloorToInt(score / scoreMilestone) * scoreMilestone;
            Instantiate(reward, rewardPos);
        }

        if (score >= lastResetScoreMedicrate + mediScoreMilestone)
        {
            lastResetScoreMedicrate = Mathf.FloorToInt(score / mediScoreMilestone) * mediScoreMilestone;
            Instantiate(medicrate, medicrateSpawnpoint);
        }
    }

}
