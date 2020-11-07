using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_flee : MonoBehaviour
{
	public Button buttonFlee;

	void Start()
	{
		Button btn = buttonFlee.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("Clicou no botao Flee!");
	}
}
