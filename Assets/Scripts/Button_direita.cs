using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_direita : MonoBehaviour
{
    public float maxHealth = 100f;
    public HealthBar healthBar;

    public Button buttonDireita;
    Button btnDireita;

    void Start()
    {
        healthBar.SetMaxHealth(healthBar.currentHealth);

        btnDireita = buttonDireita.GetComponent<Button>();
        btnDireita.onClick.AddListener(TaskOnClick);
    }
    void Update()
    {
        if (!pressed)
            return;

        //TakeDamage(healthBar.currentHealth * 0.001f);
        Debug.Log("Direita pressed");

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
        Debug.Log("You have clicked the right button!");
    }
}