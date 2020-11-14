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
    public BoardManager gameBoard = null;
    [HideInInspector]
    public RectTransform rectTransform =null;

    private int life;
    private int isOccupied;

    [HideInInspector]
    public Text textPrefabText;
    
    public void Setup(Vector2Int newBoardPosition, BoardManager newBoard){
        BoardPosition = newBoardPosition;
        gameBoard = newBoard;
        life = 1;
        isOccupied = 0;
        rectTransform = GetComponent<RectTransform>();
    }
    void Start(){

        GameObject newText = Instantiate(textPrefab, transform);
        textPrefabText = textPrefab.GetComponent<Text>();

        textPrefabText.text = life.ToString();
        
    }

    // Update is called once per frame
    void Update(){
        textPrefabText.text = life.ToString();
    }
}
