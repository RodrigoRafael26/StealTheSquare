using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_esquerda : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float maxHealth = 100f;
    public HealthBar healthBar;

    public PlayerManager player;

    void Start()
    {
        healthBar.SetMaxHealth(healthBar.currentHealth);
    }
    void Update()
    {
        if (!pressed)
            return;

        player.left();
        //TakeDamage(healthBar.currentHealth * 0.001f);

        Debug.Log("Esquerda pressed");
    }

    void TakeDamage(float damage)
    {
        healthBar.currentHealth -= damage;
        healthBar.SetHealth(healthBar.currentHealth);

    }

    bool pressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}