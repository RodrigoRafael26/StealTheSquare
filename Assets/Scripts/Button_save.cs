using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_save : MonoBehaviour
{
	public Button buttonSave;

	public XP xp_test;
	public HealthBar health;

	void Start()
	{
		Button btn = buttonSave.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("Clicou no botao Save!");
		float value = health.currentHealth / 2;
		health.currentHealth = value;
		health.SetHealth(value);
		xp_test.SetXP(value);
	}
}
