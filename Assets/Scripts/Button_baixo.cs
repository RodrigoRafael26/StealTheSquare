using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_baixo : MonoBehaviour
{
    public float maxHealth = 100f;
    public HealthBar healthBar;

    public Button buttonBaixo;
    Button btnBaixo;

    void Start()
    {

        btnBaixo = buttonBaixo.GetComponent<Button>();
        btnBaixo.onClick.AddListener(TaskOnClick);
        healthBar.SetMaxHealth(healthBar.currentHealth);
    }

    void Update()
    {
        if (!pressed)
            return;
        //TakeDamage(healthBar.currentHealth * 0.001f);

        Debug.Log("Baixo pressed");

    }

    void TakeDamage(float damage)
    {
        healthBar.currentHealth -= damage;
        healthBar.SetHealth(healthBar.currentHealth);
    }

    public bool pressed = false;
    void TaskOnClick()
    {
        pressed = true;
        Debug.Log("You have clicked the left button!");
    }
}