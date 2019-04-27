using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpInfoController : MonoBehaviour
{
    public GameObject workerInfoBox;
    public GameObject strenghtStars, dexterityStars, intelligenceStars;
    public TextMeshProUGUI nameText;

    private bool showStars;
    private void Start()
    {
        ShowWorkersInfo(0, 1, 2, "john");
    }

    public void ShowWorkersInfo(int strenght, int dexterity, int intelligence, string workerName)
    {
        nameText.text = workerName;
        workerInfoBox.SetActive(true);
        showStars = true;
        Stars(strenghtStars, strenght);
        Stars(dexterityStars, dexterity);
        Stars(intelligenceStars, intelligence);
    }

    public void CloseWorkersInfo()
    {
        workerInfoBox.SetActive(false);
        showStars = false;
    }
    void Stars(GameObject stars, int number)
    {
        foreach (Transform child in stars.transform)
        {
            child.gameObject.SetActive(false);
        }

        for (int i = 0; i< number + 1; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(showStars);
        }
    }
}
