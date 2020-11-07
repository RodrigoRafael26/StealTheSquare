using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_steal : MonoBehaviour
{
	public Button buttonSteal;

	void Start()
	{
		Button btn = buttonSteal.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("Clicou no botao Steal!");
	}
}
