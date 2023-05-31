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

    public GameObject reward;

    private int scoreMilestone = 90;
    private int lastResetScore = 0;

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score:" + score;
        pC.energy += 10;
    }

    private void Start()
    {
        reward.SetActive(true);
    }

    void Update()
    {

        if (score >= lastResetScore + scoreMilestone)
        {
            lastResetScore = Mathf.FloorToInt(score / scoreMilestone) * scoreMilestone;
            Instantiate(reward, rewardPos);
        }
    }

}
