using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class NetworkManagerSquare : NetworkManager
{
    // Start is called before the first frame update
    public Transform Position1, Position2, Position3, Position4;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform start = Position2;
        Debug.Log(start.position);
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
