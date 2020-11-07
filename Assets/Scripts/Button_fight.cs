using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_fight : MonoBehaviour
{
	public Button buttonFight;

	void Start()
	{
		Button btn = buttonFight.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("Clicou no botao Fight!");

		//pop up do fight
	}
}
