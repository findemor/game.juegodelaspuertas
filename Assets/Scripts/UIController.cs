using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public TMPro.TextMeshProUGUI TextCurrentReward;
    public TMPro.TextMeshProUGUI TextClicksLeft;
    public TMPro.TextMeshProUGUI TextDoorsOpens;

    public GameObject ButtonMenu;

    public List<GameObject> RestoreButtons;

    public void SetValues(int reward, int clicksLeft, int doorsopens)
    {
        if (clicksLeft <= 0)
        {
            TextDoorsOpens.gameObject.transform.parent.gameObject.SetActive(true);
            TextDoorsOpens.text = doorsopens.ToString();
            ButtonMenu.SetActive(true);

            foreach(GameObject go in RestoreButtons)
            {
                go.SetActive(false);
            }
        } else
        {
            TextDoorsOpens.gameObject.transform.parent.gameObject.SetActive(false);
            ButtonMenu.SetActive(false);
        }

        TextClicksLeft.text = clicksLeft.ToString();
        TextCurrentReward.text = reward.ToString();
    }

    public void ShowRestore(Puerta door)
    {
        RestoreButtons[(int)door.Type].SetActive(true);
        RestoreButtons[(int)door.Type].GetComponent<RestoreButtonController>().SetDoor(door);
    }


    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
