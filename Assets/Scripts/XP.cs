using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class XP : MonoBehaviour
{
    float xpAtual = 0.0f;
    //public Player player;
    public Text xpText;

    void Start()
    {
        writeXP(xpAtual);
    }

    public void SetXP (float xp_val)
    {
        xpAtual = xpAtual + xp_val;

        // set XP on player object
        //player.setXP((int) xp_val);

        writeXP(xpAtual);
    }

    void writeXP(float xp)
    {
        double xp1 = Math.Floor(xp);
        string xp_aux = xp1.ToString();
        xpText.text = xp_aux;
    }
}
