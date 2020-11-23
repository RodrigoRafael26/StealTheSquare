using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BoardManager : NetworkBehaviour
{

    public GameObject CellPrefab;

    public Cell[,] AllCells = new Cell[10, 10];
    //int count = 0;
    void Start(){
        
    }



    // Update is called once per frame
    void Update()
    {
        
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

        AllCells[xCell, yCell].setLife(AllCells[xCell, yCell].getLife() * 3);

        /*if (xCell == 0)
        {
            beforeX = 9;
        }
        else
        {
            beforeY = yCell - 1;
        }
        if (xCell == 0)
        {
            beforeX = 0;
        }
        else
        {
            beforeX = xCell - 1;
        }
        
        if(xCell == 9)
        {
            afterX = 0;
        }
        else
        {
            afterX = xCell + 1;
        }

        if (yCell == 9)
        {
            afterY = 0;
        }
        else
        {
            afterY = yCell + 1;
        }

        AllCells[beforeX, beforeY].setLife(AllCells[beforeX, beforeY].getLife() * 3);
        AllCells[xCell, beforeY].setLife(AllCells[xCell, beforeY].getLife() * 3);
        AllCells[afterX, beforeY].setLife(AllCells[afterX, beforeY].getLife() * 3);
        AllCells[beforeX, yCell].setLife(AllCells[beforeX, yCell].getLife() * 3);
        AllCells[xCell, yCell].setLife(AllCells[xCell, yCell].getLife() * 3);
        AllCells[afterX, yCell].setLife(AllCells[afterX, yCell].getLife() * 3);
        AllCells[beforeX, afterY].setLife(AllCells[beforeX, afterY].getLife() * 3);
        AllCells[xCell, afterY].setLife(AllCells[xCell, afterY].getLife() * 3);
        AllCells[afterX, afterY].setLife(AllCells[afterX, afterY].getLife() * 3);*/
    }
}
