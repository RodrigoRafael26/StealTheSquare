﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerManager : NetworkBehaviour {

    // Start is called before the first frame update
    public Vector3 movement;
    public override void OnStartServer(){
        base.OnStartServer();
        Debug.Log("Server started");
    }
    public int n_width = 300;
    public int n_height = 400;
    private Vector2 gridPos;

    // Update is called once per frame
    [Client]
    void Update()
    {
        if(!hasAuthority){ return;} // only control one player
     
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gridPos.y+=10;
            Debug.Log("GOING UP");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gridPos.x-=10;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gridPos.x+= 10;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gridPos.y-= 10;
        }

       
        transform.position = new Vector3(gridPos.x, gridPos.y);

    }
}
