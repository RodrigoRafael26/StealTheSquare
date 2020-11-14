using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Cell : NetworkBehaviour {

    // Start is called before the first frame update
    public Sprite image;
    [HideInInspector]
    public Vector2Int BoardPosition = Vector2Int.zero;

    [HideInInspector]
    public BoardManager gameBoard = null;
    [HideInInspector]
    public RectTransform rectTransform =null;

    private int life;
    private int isOccupied;
    
    
    public void Setup(Vector2Int newBoardPosition, BoardManager newBoard){
        BoardPosition = newBoardPosition;
        gameBoard = newBoard;
        life = 0;
        isOccupied = 0;
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
