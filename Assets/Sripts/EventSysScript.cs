using UnityEngine;
using UnityEngine.EventSystems;

public class EventSysScript : MonoBehaviour
{
    public PauseScript pauseScript;

    public GameObject perkSelect, pauseFirst, optionsFirst, optionsClosed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void PauseFirst()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirst);
    }

    public void PerkSelect()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(perkSelect);
    }

    public void OptionsFirst()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirst);
    }

    public void OptionsClosed()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosed);
    }
}
