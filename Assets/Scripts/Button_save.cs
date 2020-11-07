using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_save : MonoBehaviour
{
	public Button buttonSave;

	void Start()
	{
		Button btn = buttonSave.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("Clicou no botao Save!");
	}
}
