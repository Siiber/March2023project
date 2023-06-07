using UnityEngine;
using UnityEngine.UI;

public class UInstantiator : MonoBehaviour
{
    public GameObject weaponcheckmark1;
    public GameObject weaponcheckmark2;

    private GameObject instantiatedWeaponCheckmark1;
    private GameObject instantiatedWeaponCheckmark2;

    public GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GM").GetComponent<GameManager>();
        Instantiator();
    }

    private void Instantiator()
    {
        instantiatedWeaponCheckmark1 = Instantiate(weaponcheckmark1, transform);
        instantiatedWeaponCheckmark2 = Instantiate(weaponcheckmark2, transform);
    }

    void Update()
    {
        if (gm.rifle)
        {
            instantiatedWeaponCheckmark1.SetActive(true);
            instantiatedWeaponCheckmark2.SetActive(false);
        }

        if (gm.shotgun)
        {
            instantiatedWeaponCheckmark1.SetActive(false);
            instantiatedWeaponCheckmark2.SetActive(true);
        }
    }
}
