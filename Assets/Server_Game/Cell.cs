using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Mirror;

public class Cell : NetworkBehaviour {

    // Start is called before the first frame update
    public Sprite image;
    
    public GameObject textPrefab;
    [HideInInspector]
    public Vector2Int BoardPosition = Vector2Int.zero;
    [HideInInspector]
    public RectTransform rectTransform =null;

    [SyncVar]
    int life;
    private int isOccupied;

    [HideInInspector]
    public Text textPrefabText;
    
    public void Setup(Vector2Int newBoardPosition, int given_life)
    {
        BoardPosition = newBoardPosition;
        life = given_life;
        isOccupied = 0;
        rectTransform = GetComponent<RectTransform>();
    }
    void Start(){
        
        GameObject newText = Instantiate(textPrefab, transform);
        newText.AddComponent<NetworkIdentity>();
        textPrefabText = newText.GetComponent<Text>();

        textPrefabText.text = life.ToString();
        
    }
    
    
    public void setLife(int new_life)
    {
        this.life = new_life;
        textPrefabText.text = this.life.ToString();
    }

    public int getLife()
    {
        return this.life;
    }

    // Update is called once per frame
    void Update(){
        textPrefabText.text = life.ToString();
    }
}
