using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggles_Fight : MonoBehaviour
{
    GameObject rock;
    GameObject paper;
    GameObject scissor;
    GameObject popupFight;
    public Button buttonOK;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = buttonOK.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        rock = GameObject.Find("rock");
        paper = GameObject.Find("paper");
        scissor = GameObject.Find("scissor");
        popupFight = GameObject.Find("PopupFight");
    }

    void TaskOnClick()
    {
        if (!rock.GetComponent<Toggle>().isOn && !paper.GetComponent<Toggle>().isOn && !scissor.GetComponent<Toggle>().isOn)
        {
            popupFight.SetActive(true);
        }
        if (rock.GetComponent<Toggle>().isOn)
        {
            Debug.Log("Rock was selected!");
        }
        if (paper.GetComponent<Toggle>().isOn)
        {
            Debug.Log("Paper was selected!");
        }
        if (scissor.GetComponent<Toggle>().isOn)
        {
            Debug.Log("Scissors was selected!");
        }
    }

}
