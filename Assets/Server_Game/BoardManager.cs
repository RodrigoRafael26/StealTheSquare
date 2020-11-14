using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BoardManager : NetworkBehaviour
{
    // Start is called before the first frame update

    public GameObject CellPrefab;
    public Cell[,] AllCells = new Cell[10, 10];


    void Start()
    {
        //Create board matrix
        for (int x = 0; x < 10; x++){
                for(int y = 0; y < 10; y++){
                    GameObject newCell = Instantiate(CellPrefab, transform);
                    RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                    Debug.Log(rectTransform);
                    rectTransform.anchoredPosition = new Vector2((x*100)+50, (y*100)+50);
                    
                    AllCells[x,y] = newCell.GetComponent<Cell>();
                    AllCells[x,y].Setup(new Vector2Int(x,y),this);
                }
            }
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
