using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_cima : MonoBehaviour
{
    public float maxHealth = 100f;
    public HealthBar healthBar;

    public Button buttonCima;
    Button btnCima;

   void Start()
    {
        healthBar.SetMaxHealth(healthBar.currentHealth);

        btnCima = buttonCima.GetComponent<Button>();
        btnCima.onClick.AddListener(TaskOnClick);
    }
    void Update()
    {
        if (!pressed)
            return;

        //TakeDamage(healthBar.currentHealth * 0.001f);
        Debug.Log("Cima pressed");

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
        Debug.Log("You have clicked the upper button!");
    }
}