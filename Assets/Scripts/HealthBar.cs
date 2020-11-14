using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public float currentHealth = 100f;
    public Player player;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
        
    public void SetHealth(float health)
    {
        slider.value = health;

        // set HP on player object
        player.setHealth(health);
    }
}
