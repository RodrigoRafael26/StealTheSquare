using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

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
        
        
        int num= 0;
        for (int x = 0; x < 10; x++){
            for(int y = 0; y < 10; y++){
                AllCells[x,y] = Cells[num].GetComponent<Cell>();
                Cells[num].transform.SetParent(Canvas.transform);
                num++;
            }
        }
        
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
    void Update()
    {

        gridPos = transform.position;
        float health = getHealth();
        if (!hasAuthority){ return;} // only control one player

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gridPos.y+=100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
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

    [Command]
    void Sow(float x, float y){

        Debug.Log(x.ToString());
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

    [Command]
    public void doHarvest(float x, float y)
    {
        int xCell = 5, yCell = 5;
        float cellLife;
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

        cellLife = AllCells[xCell, yCell].getLife();
        setHealth(getHealth() + cellLife);       
        AllCells[xCell, yCell].setLife(0);
    }
}
