using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour{
    
    // Start is called before the first frame update
    public int n_width= 10;
    public int n_height = 10;
    private Vector2 gridPos;
    void Start()
    {
        float x = (float) Random.Range(n_width*(-1) ,n_width);
        float y = (float) Random.Range(n_height*(-1) ,n_height);;
        gridPos = new Vector2 (x + 0.5f , y + 0.5f);
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gridPos.y++;
            Debug.Log("Cima");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gridPos.x--;
            Debug.Log("Esquerda");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gridPos.x++;
            Debug.Log("Direita");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gridPos.y--;
            Debug.Log("Baixo");
        }

        if (gridPos.x < -5)
        {
            gridPos.x = 4.5f;
        }
        else if (gridPos.x > 5)
        {
            gridPos.x = -4.5f;
        }
        else if (gridPos.y < -5)
        {
            gridPos.y = 4.5f;
        }
        else if (gridPos.y > 5)
        {
            gridPos.y = -4.5f;
        }

        transform.position = new Vector3(gridPos.x, gridPos.y);

    }
}
