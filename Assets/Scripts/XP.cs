using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XP : MonoBehaviour
{
    int xpAtual = 0;

    public Text xpText;

    void Start()
    {
        writeXP(xpAtual);
    }

    public void SetXP (int xp_val)
    {
        if ((xpAtual + xp_val) > 100)
        {
            xpAtual = 100;
        }
        else
        {
            xpAtual = xpAtual + xp_val;
        }
        
        writeXP(xpAtual);
    }

    void writeXP(int xp)
    {
        string xp_aux = xp + "/" + "100";
        xpText.text = xp_aux;
    }
}
