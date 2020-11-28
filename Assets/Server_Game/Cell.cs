using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Mirror;

public class Cell : NetworkBehaviour {
    
    public GameObject textPrefab;
    [HideInInspector]
    public Vector2Int BoardPosition = Vector2Int.zero;
    [HideInInspector]
    public RectTransform rectTransform =null;

    [SyncVar]
    float life;

    //TODO MUDAR PARA LISTA em vez de playerID
    [SyncVar]
    public List<int> players;
    
    int life_toPrint = 1;

    [HideInInspector]
    public Text textPrefabText;

    float count = 0f;
    
    public Sprite FULL, HALF_LOW, HALF_HIGH, EMPTY;
    public void Setup(Vector2Int newBoardPosition, int given_life)
    {
        BoardPosition = newBoardPosition;
        life = given_life;
        players = new List<int>();
        rectTransform = GetComponent<RectTransform>();

        Debug.Log(this);
    }

    void Start(){

        this.GetComponent<Image>().sprite = EMPTY;
        GameObject newText = Instantiate(textPrefab, transform);
        newText.AddComponent<NetworkIdentity>();
        textPrefabText = newText.GetComponent<Text>();
        

        textPrefabText.text = life_toPrint.ToString();
    }
    
    
    public void setLife(float new_life)
    {
        this.life = new_life;
        life_toPrint = (int)Math.Round(this.life);
        textPrefabText.text = life_toPrint.ToString();
    }

    public float getLife()
    {
        return this.life;
    }

    // Update is called once per frame
    void Update(){
        float life_now = getLife();

        if (count > 20.0f)
        {
            count = 0;
            setLife(life_now + life_now * 0.1f);
        }
        else
        {
            setLife(life_now);
        }
        count += Time.deltaTime;
    }

}
