using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LobbyUI : MonoBehaviour
{
    public void ExitGameRoom()
    {
        var manager = RoomManager.singleton;
        if (manager.mode == NetworkManagerMode.Host)
        {
            manager.StopHost();
        }
        else if (manager.mode == NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        }
    }
}