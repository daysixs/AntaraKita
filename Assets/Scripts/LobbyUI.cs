using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    public static LobbyUI instance;

    [SerializeField]
    private TMP_Text playerCountText;

    public void UpdatePlayerCount()
    {
        var manager = NetworkManager.singleton as RoomManager;
        var players = FindObjectsOfType<RoomPlayer>();
        playerCountText.text = string.Format("{0}/{1}", players.Length, manager.maxConnections);
    }

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