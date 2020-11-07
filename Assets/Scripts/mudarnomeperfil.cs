using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mudarnomeperfil : MonoBehaviour
{ 
    public string nomejogador;
    public string guardarnome;

    public Text inputText;
    public Text loadedname;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nomejogador = PlayerPrefs.GetString("name");
        loadedname.text = nomejogador;


    }

    public void SetName()
    {
        guardarnome = inputText.text;
        PlayerPrefs.SetString("name", guardarnome);

    }
}
