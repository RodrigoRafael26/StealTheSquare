using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float time = 0f;
    float initialTime = 300f;

    public Text timerText;

    void Start()
    {
        time = initialTime; 
    }

    void Update()
    {
        if (time > 1.0f)
        {
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
