using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 gridPos;
    void Start()
    {
        gridPos = new Vector2 (0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
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
