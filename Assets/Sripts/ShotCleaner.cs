using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCleaner : MonoBehaviour
{
    public float cleaningTime = 6f;
    public bool perkGuideCleaner = false;
    public ButtonManager bm;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, cleaningTime);
    }

    void Update()
    {
        if (perkGuideCleaner) 
        {
            bm = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
            if (bm.perkguiderOFF)
                Destroy(gameObject);
        }
    }

}
