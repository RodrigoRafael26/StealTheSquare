using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

class PlayerClass {
    public string nickname;
    public float health;
    public int xp;
    public bool[,] position;
}

public class PlayerManager : NetworkBehaviour {

    // Start is called before the first frame update
    public Vector3 movement;
    public GameObject CellPrefab;
    [HideInInspector]
    public GameObject Canvas;
    public Cell[,] AllCells = new Cell[10, 10];
    PlayerClass player;
    [HideInInspector]
    public NetworkManagerSquare board;

    public override void OnStartClient(){
        base.OnStartClient();
        Canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
        GameObject[] Cells = GameObject.FindGameObjectsWithTag("Board");

        GameObject[] Botoes = GameObject.FindGameObjectsWithTag("Botao");

        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");

        GameObject[] Camera = GameObject.FindGameObjectsWithTag("MainCamera");


        int num = 0;
        for (int x = 0; x < 10; x++){
            for(int y = 0; y < 10; y++){
                AllCells[x,y] = Cells[num].GetComponent<Cell>();
                Cells[num].transform.SetParent(Canvas.transform);
                num++;
            }
        }

        //ir a todos os botoes e meter o player como parent dos botoes
        foreach (GameObject botao in Botoes)
        {
            botao.transform.SetParent(Player[0].transform);
            botao.transform.position = Player[0].transform.position;
        }

        Camera[0].transform.SetParent(Player[0].transform);
        Camera[0].transform.position = Player[0].transform.position;

        //enterCell(gridPos);
    }

    public override void OnStartServer(){
        base.OnStartServer();
        Canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
        DrawMap();
    }

    private Vector3 gridPos;
    void Start() {
        player = new PlayerClass();
        player.health = 100f;
        player.xp = 0;
    }

    public void setHealth(float health) {
        player.health = health;
    }

    public void setXP(int xp){
        player.xp = xp;
    }

    public void setNickname(string nickname){
        player.nickname = nickname;
    }

    public float getHealth(){
        return player.health;
    }

    public int getXP(){
        return player.xp;
    }

    public string getNickname(){
        return player.nickname;
    }

    // Update is called once per frame
    [Client]
    public void Update()
    {
        gridPos = transform.position;
        float health = getHealth();
        if (!hasAuthority){ return;} // only control one player
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            leaveCell(gridPos);
            gridPos.y+=100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
            if (enterCell(gridPos))
            {
                Debug.Log("Collided");
            }
            else
            {
                Debug.Log("Didnt collided");
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gridPos.x-=100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gridPos.x+= 100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gridPos.y-= 100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
        }
        
        else if (Input.GetKeyDown(KeyCode.S))
        {
            float half_health = health / 2;
            int xp = getXP();
            setHealth(half_health);
            setXP(xp + (int)half_health);
            //Debug.Log(getXP());
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            float third_health = health / 3;
            setHealth(health - third_health);
            //Debug.Log("Life is now "+  getHealth().ToString());
            Sow(gridPos.x, gridPos.y);    
        }
        
       
        if (gridPos.y > 500) gridPos.y = -450;
        if(gridPos.y < -500) gridPos.y = 450;
        if(gridPos.x > 500) gridPos.x = -450;
        if(gridPos.x < -500) gridPos.x = 450;

        transform.position = new Vector3(gridPos.x, gridPos.y);

        Debug.Log("Health: " + getHealth());
        Debug.Log("XP: " + getXP());
    }

    public void up()
    {
        //float health = getHealth();
        gridPos.y += 100;
        //setHealth(health - health * 0.001f);
        doHarvest(gridPos.x, gridPos.y);
    }

    public void down()
    {
        Debug.Log("olaaaaaaaaaaa senhor");
        //float health = getHealth();
        gridPos.y -= 100;
        //setHealth(health - health * 0.001f);
        doHarvest(gridPos.x, gridPos.y);
    }

    public void left()
    {
        //float health = getHealth();
        gridPos.x -= 100;
        //setHealth(health - health * 0.001f);
        doHarvest(gridPos.x, gridPos.y);
    }

    public void right()
    {
        //float health = getHealth();
        gridPos.x += 100;
        //setHealth(health - health * 0.001f);
        doHarvest(gridPos.x, gridPos.y);
    }

    private bool enterCell(Vector3 gridPos)
    {
        int xCell = switchFunction(gridPos.x), yCell = switchFunction(gridPos.y);
        if(AllCells[xCell, yCell].isOccupied)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void leaveCell(Vector3 gridPos)
    {
        int xCell = switchFunction(gridPos.x), yCell = switchFunction(gridPos.y);
        AllCells[xCell, yCell].isOccupied = false;
    }

    [Command]
    void DrawMap(){
        for (int x = 0; x < 10; x++){
            for(int y = 0; y < 10; y++){
                GameObject newCell = Instantiate(CellPrefab, Canvas.transform);
                NetworkServer.Spawn(newCell);
                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);
                
                AllCells[x,y] = newCell.GetComponent<Cell>();
                AllCells[x,y].Setup(new Vector2Int(x,y), 1);
            }
        }
    }

    int switchFunction(float number)
    {
        switch (number)
        {
            case 450.0f:
                return 9;
            case 350.0f:
                return 8;
            case 250.0f:
                return 7;
            case 150.0f:
                return 6;
            case 50.0f:
                return 5;
            case -50.0f:
                return 4;
            case -150.0f:
                return 3;
            case -250.0f:
                return 2;
            case -350.0f:
                return 1;
            case -450.0f:
                return 0;
            default:
                return 0;
        }
    }

    [Command]
    void Sow(float x, float y){
        int xCell = switchFunction(x), yCell = switchFunction(y);
        AllCells[xCell, yCell].setLife(AllCells[xCell, yCell].getLife() + 4);
    }

    [Command]
    public void doHarvest(float x, float y)
    {
        Debug.Log("aaadeus");
        int xCell = switchFunction(x), yCell = switchFunction(y);
        float cellLife;

        cellLife = AllCells[xCell, yCell].getLife();
        setHealth(getHealth() + cellLife);       
        AllCells[xCell, yCell].setLife(0);
    }
}
