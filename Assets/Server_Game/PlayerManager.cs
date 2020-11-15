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
    PlayerClass player;
    public BoardManager board;

    public override void OnStartServer(){
        base.OnStartServer();
        Debug.Log("Server started");
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
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gridPos.x-=100;
            setHealth(health - health * 0.001f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gridPos.x+= 100;
            setHealth(health - health * 0.001f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gridPos.y-= 100;
            setHealth(health - health * 0.001f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            float half_health = health / 2;
            int xp = getXP();
            setHealth(half_health);
            setXP(xp + (int)half_health);
            Debug.Log(getXP());
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            float third_health = health / 3;
            setHealth(health - third_health);
            // RICKKKKKKKKKKYYYYYYYYYYYYYYYYYYYYYYY falta aceder a este board
            // board.doSow(gridPos.x, gridPos.y);    
        }

        if(gridPos.y > 500) gridPos.y = -450;
        if(gridPos.y < -500) gridPos.y = 450;
        if(gridPos.x > 500) gridPos.x = -450;
        if(gridPos.x < -500) gridPos.x = 450;

        transform.position = new Vector3(gridPos.x, gridPos.y);

        // Debug.Log("Health: " + getHealth());
        // Debug.Log("XP: " + getXP());
    }
}
