using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour
{
    public SpriteRenderer board;
    public int squares_w;
    public int squares_h;
    // Start is called before the first frame update
    void Start(){
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = (float) ((board.bounds.size.x/squares_w)*3) / (float)((board.bounds.size.y/squares_h)*3);

        if(screenRatio >= targetRatio){
            Camera.main.orthographicSize = ((board.bounds.size.y/squares_h)*3)/2;
        }else{
            float diff = targetRatio / screenRatio;
            Camera.main.orthographicSize =((board.bounds.size.y/squares_h)*3)/2 * diff;
        }
    }

    // Update is called once per frame
    void Update(){
        
    }
}
