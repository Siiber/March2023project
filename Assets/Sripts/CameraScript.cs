using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CameraScript : MonoBehaviour
{
    public ScoreSys scoreSys;
    public int colorChangeThreshold;
    [HideInInspector]
    public int thresholdReset= 0;
    public Color[] targetColor;

    private Camera cameraComponent;
    private Color InitialColor;

    // Start is called before the first frame update
    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        InitialColor = cameraComponent.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreSys.score >= thresholdReset + colorChangeThreshold)
        {
            thresholdReset = Mathf.FloorToInt(scoreSys.score / colorChangeThreshold) * colorChangeThreshold;
            int randomIndex = Random.Range(0, targetColor.Length);
            cameraComponent.backgroundColor = targetColor[randomIndex];
        }
    }
}
