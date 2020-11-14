using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerClass {
    public string nickname;
    public float health;
    public int xp;
}

public class Player : MonoBehaviour
{
    PlayerClass player;

    // Start is called before the first frame update
    void Start() {
        player = new PlayerClass();
        player.health = 100f;
        player.xp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("GOING UP");
            player.health -= 5;
            Debug.Log(player.health);    
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.health -= 5;
            Debug.Log(player.health);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.health -= 5;
            Debug.Log(player.health);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.health -= 5;
            Debug.Log(player.health);
        }
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
}
