using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float time = 0f;
    float initialTime = 300f;

    public Text timerText;

    float count = 0.0f;
    public HealthBar health;

    void Start()
    {
        time = initialTime; 
    }

    void Update()
    {
        if (time > 1.0f)
        {
            if (count > 5.0f)
            {
                float value = (float) (health.currentHealth - health.currentHealth*0.01);
                health.currentHealth = value;
                health.SetHealth(value);
                count = 0;
            }
            count += Time.deltaTime;
            time -= Time.deltaTime;
            int min = Mathf.FloorToInt(time / 60F);
            int sec = Mathf.FloorToInt(time - min * 60);
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        else
        {
            time = 0f;
        }

    }
}
