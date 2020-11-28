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


public class Collision {
    public int id_1;
    public int id_2;
    public int collisionID;
    public bool resolved;
}

public class PlayerManager : NetworkBehaviour {

    Button_cima cima;

    // Start is called before the first frame update
    public Vector3 movement;
    public GameObject CellPrefab;
    [HideInInspector]
    public GameObject Canvas;
    public Cell[,] AllCells = new Cell[10, 10];
    PlayerClass player;
    [HideInInspector]
    public NetworkManagerSquare board;
    public float health;
    GameObject[] botoesMove;

    [HideInInspector]
    public List<Collision> serverCollisions = new List<Collision>();
    [SyncVar]
    public int num_players = 0;

    public int collisionID = 0;

    
    public override void OnStartClient(){
        num_players++;
        base.OnStartClient();
        Canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
        GameObject[] Cells = GameObject.FindGameObjectsWithTag("Board");

        GameObject[] Botoes = GameObject.FindGameObjectsWithTag("Game");

        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");

        GameObject Camera = GameObject.FindGameObjectsWithTag("MainCamera")[0];


        botoesMove = GameObject.FindGameObjectsWithTag("BotoesMove");

        int num = 0;
        for (int x = 0; x < 10; x++){
            for(int y = 0; y < 10; y++){
                AllCells[x,y] = Cells[num].GetComponent<Cell>();
                Cells[num].transform.SetParent(Canvas.transform);
                num++;
            }
        }

  
        
         //vai ser preciso verificar qual é o local player
        foreach (GameObject player in Player){
            NetworkBehaviour net = player.GetComponent<NetworkBehaviour>();
            if(net.isLocalPlayer){
                Debug.Log("Local Player");
                Camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
                Camera.transform.SetParent(player.transform);
                Botoes[0].transform.SetParent(player.transform);
                Botoes[0].transform.position = new Vector3(player.transform.position.x, player.transform.position.y,0);
               
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
        health =  player.health;
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
    public void Update()
    {
        gridPos = transform.position;
        health = getHealth();
        if (!hasAuthority){ return;} // only control one player

        if (botoesMove[0].GetComponent<Button_cima>().pressed)
            
        {
            botoesMove[0].GetComponent<Button_cima>().pressed = false;
            leaveCell(gridPos);
            gridPos.y+=100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
            //enterCell(gridPos);
        }
        else if (botoesMove[3].GetComponent<Button_esquerda>().pressed)
        {
            botoesMove[3].GetComponent<Button_esquerda>().pressed = false;
            leaveCell(gridPos);
            gridPos.x-=100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
            //enterCell(gridPos);
            
        }
        else if (botoesMove[2].GetComponent<Button_direita>().pressed)
        {
            botoesMove[2].GetComponent<Button_direita>().pressed = false;
            leaveCell(gridPos);
            gridPos.x+= 100;
            setHealth(health - health * 0.001f);
            doHarvest(gridPos.x, gridPos.y);
            //enterCell(gridPos);
        }
        else if (botoesMove[1].GetComponent<Button_baixo>().pressed)
        {
            botoesMove[1].GetComponent<Button_baixo>().pressed = false;
            leaveCell(gridPos);
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
        enterCell(gridPos);
        transform.position = new Vector3(gridPos.x, gridPos.y);

        //Debug.Log("Health: " + getHealth());
        //Debug.Log("XP: " + getXP());
    }

    [Command]
    private void enterCell(Vector3 gridPos)
    {
        int xCell = switchFunction(gridPos.x), yCell = switchFunction(gridPos.y);
        if(AllCells[xCell, yCell].players.Count == 2)
        {
            //Criar uma lista de colisioes para aceitar sempre a primeira resposta
            RPCenableActionButtons(AllCells[xCell, yCell].players.ToArray()[0], AllCells[xCell, yCell].players.ToArray()[1], collisionID);
            //Create a new collision
            Collision col = new Collision();
            //add both id's in the list to the collision
            col.id_1 = AllCells[xCell, yCell].players.ToArray()[0];
            col.id_2 = AllCells[xCell, yCell].players.ToArray()[1];
            col.collisionID = collisionID;
            col.resolved = false;
            serverCollisions.Add(col);
            //aumentar o collision ID
            collisionID++;
        }
        
        AllCells[xCell, yCell].players.Add(player.ID);

    }

    [Command]
    private void leaveCell(Vector3 gridPos)
    {
        int xCell = switchFunction(gridPos.x), yCell = switchFunction(gridPos.y);
        AllCells[xCell, yCell].players.Remove(player.ID);
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
    public void RPCenableActionButtons(int id_1, int id_2, int collision){
        //block movement until button press or collision solved
        Debug.Log("SHOW BUTTONS");

        //precisamos de receber a escolha da acao aqui
    }

    [Command]
    public void playerDecision(int playerID, int playerDecision, int CollisionID){
        //verificar se aquela colisao ja foi resolvida procurando na lista pelo ID
        Collision handle = serverCollisions.Find(x => x.collisionID == collisionID);

        //se sim return e nao faz nada
        if(handle != null && handle.resolved == true) return;
        else{
            //se nao fazer enviar a resposta para todos
            RPCHandleCollision(handle.id_1, handle.id_2, playerDecision);
            handle.resolved = true;
        }
        
    }

    [ClientRpc]
    public void RPCHandleCollision(int id_1, int id_2, int collisionType){
        //verificar se o local player faz parte dos jogadores da colisao
        if(player.ID == id_1 || player.ID == id_2){
            Debug.Log("COLLISION");
            //if(fight) do fight random (fazer a cena do pedra/papel/tesoura da muito trabalho para agr) ...
        }else{
            return;
        }
    }
}
