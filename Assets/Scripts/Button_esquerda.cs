using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_esquerda : MonoBehaviour
{
    public float maxHealth = 100f;
    public HealthBar healthBar;

    public Button buttonEsquerda;
    Button btnEsquerda;

    void Start()
    {
        healthBar.SetMaxHealth(healthBar.currentHealth);

        btnEsquerda = buttonEsquerda.GetComponent<Button>();
        btnEsquerda.onClick.AddListener(TaskOnClick);
    }
    void Update()
    {
        if (!pressed)
            return;

        //TakeDamage(healthBar.currentHealth * 0.001f);
        Debug.Log("Esquerda pressed");
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