using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomPlayer : NetworkRoomPlayer
{

    public GameObject ruleSettingButton;

    public void Open()
    {
        MyRoomPlayer.lobbyPlayer.IsMoving = false;
        ruleSettingButton.SetActive(false);
    }

    public void Close()
    {
        MyRoomPlayer.lobbyPlayer.IsMoving = true;
        ruleSettingButton.SetActive(true);

    }

    private static RoomPlayer myRoomPlayer;
    
    public static RoomPlayer MyRoomPlayer
    {
        get
        {
            if(myRoomPlayer == null)
            {
                var players = FindObjectsOfType<RoomPlayer>();
                foreach(var player in players)
                {
                    if(player.isOwned)
                    {
                        myRoomPlayer = player;
                    }
                }
            }
            return myRoomPlayer;
        }
    }



    [SyncVar]
    public EPlayerColor playerColor;

    public PlayerMovement lobbyPlayer;

    private void Start()
    {
        base.Start();

        if (isServer)
        {
            SpawnPlayerInLobby();
        }
        else
        {
            ruleSettingButton.SetActive(false);
        }

    }

    private void SpawnPlayerInLobby()
    {
        var roomSlots = (NetworkManager.singleton as RoomManager).roomSlots;

        EPlayerColor color = EPlayerColor.Red;

        for (int i = 0; i < (int)EPlayerColor.Lime + 1; i++)
        {
            bool isSameColor = false;
            foreach (var roomPlayer in roomSlots)
            {
                var roomP = roomPlayer as RoomPlayer;
                if (roomP.playerColor == (EPlayerColor)i && roomPlayer.netId != netId)
                {
                    isSameColor = true;
                    break;
                }
            }

            if (!isSameColor)
            {
                color = (EPlayerColor)i;
                break;
            }
        }

        playerColor = color;

        Vector3 spawnPos = FindObjectOfType<SpawnPositions>().GetSpawnPos();

        var playerChar = Instantiate(RoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyPlayerRestrict>();

        NetworkServer.Spawn(playerChar.gameObject, connectionToClient);

        playerChar.ownerNetId = netId;

        playerChar.playerColor = color;
    }
}