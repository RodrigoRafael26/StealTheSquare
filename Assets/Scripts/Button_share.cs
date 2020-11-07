using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_share : MonoBehaviour
{
	public Button buttonShare;

	void Start()
	{
		Button btn = buttonShare.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("Clicou no botao Share!");
	}
}
