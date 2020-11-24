using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

class PlayerClass {
    public string nickname;
    public int ID;
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

    [SyncVar]
    public int num_players = 0;

    public override void OnStartClient(){
        
        base.OnStartClient();
        Canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
        GameObject[] Cells = GameObject.FindGameObjectsWithTag("Board");
        
        
        int num = 0;
        for (int x = 0; x < 10; x++){
            for(int y = 0; y < 10; y++){
                AllCells[x,y] = Cells[num].GetComponent<Cell>();
                Cells[num].transform.SetParent(Canvas.transform);
                num++;
            }
        }

        //enterCell(gridPos);
    }

    public override void OnStartServer(){
        num_players++;
        base.OnStartServer();
        Canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
        DrawMap();
    }

    private Vector3 gridPos;
    void Start() {
        player = new PlayerClass();
        player.health = 100f;
        player.xp = 0;
        player.ID = num_players;

        Debug.Log(player.ID );
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
    void Update()
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
            enterCell(gridPos);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leaveCell(gridPos);
            gridPos.x-=100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
            enterCell(gridPos);
            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            leaveCell(gridPos);
            gridPos.x+= 100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
            enterCell(gridPos);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            leaveCell(gridPos);
            gridPos.y-= 100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
            enterCell(gridPos);
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

        //Debug.Log("Health: " + getHealth());
        //Debug.Log("XP: " + getXP());
    }

    [Command]
    private void enterCell(Vector3 gridPos)
    {
        int xCell = switchFunction(gridPos.x), yCell = switchFunction(gridPos.y);
        if(AllCells[xCell, yCell].playerID != 0)
        {
            enableActionButtons(AllCells[xCell, yCell].playerID, player.ID);
        }
        
        AllCells[xCell, yCell].playerID = player.ID;

    }

    [Command]
    private void leaveCell(Vector3 gridPos)
    {
        int xCell = switchFunction(gridPos.x), yCell = switchFunction(gridPos.y);
        AllCells[xCell, yCell].playerID = 0;
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
        int xCell = switchFunction(x), yCell = switchFunction(y);
        float cellLife;

        cellLife = AllCells[xCell, yCell].getLife();
        setHealth(getHealth() + cellLife);       
        AllCells[xCell, yCell].setLife(0);
    }

    [ClientRpc]
    public void enableActionButtons(int id_1, int id_2){
        Debug.Log("COLLISION");
    }
}
