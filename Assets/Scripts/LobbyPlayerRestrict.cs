using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LobbyPlayerRestrict : PlayerMovement
{
    [SyncVar(hook = nameof(HandleOwnerNetId))]

    public uint ownerNetId;

    public void HandleOwnerNetId(uint oldId, uint newId)
    {
        var players = FindObjectsOfType<RoomPlayer>();
        foreach(var player in players)
        {
            if(newId == player.netId)
            {
                player.lobbyPlayer = this;
                break;
            }
        }
    }

    public void CompleteSpawn()
    {
        if (isOwned)
        {
            IsMoving = true;
        }
    }
}