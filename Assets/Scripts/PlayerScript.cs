using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour{
    
    // Start is called before the first frame update
    public int n_width= 10;
    public int n_height = 10;
    private Vector3 gridPos;
    void Start()
    {
        gridPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gridPos.y++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gridPos.x--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gridPos.x++;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gridPos.y--;
        }


       // transform.position = new Vector3(gridPos.x, gridPos.y);

    }
}
