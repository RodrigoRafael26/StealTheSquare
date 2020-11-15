using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class NetworkManagerSquare : NetworkManager
{
    // Start is called before the first frame update

    public GameObject Canvas;
    public Cell[,] AllCells = new Cell[10, 10];
    //int count = 0;
    void Start(){
        Canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
    }

    
    public void doSow(float x, float y)
    {

        int xCell = 5, yCell = 5;
        switch (x)
        {
            case 450.0f:
                xCell = 9;
                break;
            case 350.0f:
                xCell = 8;
                break;
            case 250.0f:
                xCell = 7;
                break;
            case 150.0f:
                xCell = 6;
                break;
            case 50.0f:
                xCell = 5;
                break;
            case -50.0f:
                xCell = 4;
                break;
            case -150.0f:
                xCell = 3;
                break;
            case -250.0f:
                xCell = 2;
                break;
            case -350.0f:
                xCell = 1;
                break;
            case -450.0f:
                xCell = 0;
                break;
            default:
                break;
        }

        switch (y)
        {
            case 450.0f:
                yCell = 9;
                break;
            case 350.0f:
                yCell = 8;
                break;
            case 250.0f:
                yCell = 7;
                break;
            case 150.0f:
                yCell = 6;
                break;
            case 50.0f:
                yCell = 5;
                break;
            case -50.0f:
                yCell = 4;
                break;
            case -150.0f:
                yCell = 3;
                break;
            case -250.0f:
                yCell = 2;
                break;
            case -350.0f:
                yCell = 1;
                break;
            case -450.0f:
                yCell = 0;
                break;
            default:
                break;
        }
        
        AllCells[xCell, yCell].setLife(AllCells[xCell, yCell].getLife() + 4);

    }
}
