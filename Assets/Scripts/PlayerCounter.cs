using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerCounter : NetworkBehaviour
{
    [SyncVar]
    private int minPlayer;
    [SyncVar]
    private int maxPlayer;

    [SerializeField]
    private TMP_Text playerCountText;


    public void UpdatePlayerCount()
    {
        
        var players = FindObjectsOfType<RoomPlayer>();
        bool isStartable = players.Length >= minPlayer;
        playerCountText.color = isStartable ? Color.white : Color.red;
        playerCountText.text = string.Format("{0}/{1}", players.Length, maxPlayer);
        LobbyUI.instance.SetIntertract(isStartable);
    }

    public override void OnStartAuthority()
    {
        if (isServer)
        {
            var manager = NetworkManager.singleton as RoomManager;
            minPlayer = manager.minPlayerCount;
            maxPlayer = manager.maxConnections;
        }
    }
}
