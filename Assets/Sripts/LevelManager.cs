using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PauseScript pS;
    
    public float transitionTime= 0.2f;
    public Animator fadeScreen;

    private void Start()
    {
        pS = GetComponent<PauseScript>();
        fadeScreen= GameObject.Find("FadeScreen").GetComponent<Animator>();
    }

    public void ChangeLevel(int levelNumber)
    {
        StartCoroutine(LoadLevel(levelNumber));
    }

    public IEnumerator LoadLevel(int levelToLoad)
    {
        print("buttonpress");
        if (!pS == null)
        {
            pS.paused = false;
        }
        fadeScreen.SetTrigger("ChangeLevel");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelToLoad);
    }

}
