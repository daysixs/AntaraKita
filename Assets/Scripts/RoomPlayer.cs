using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public EPlayerColor playerColor;

    public PlayerMovement lobbyPlayer;


    private static RoomPlayer myRoomPlayer;

    public static RoomPlayer MyRoomPlayer
    {
        get
        {
            if (myRoomPlayer == null)
            {
                var players = FindObjectsOfType<RoomPlayer>();
                foreach (var player in players)
                {
                    if (player.isOwned)
                    {
                        myRoomPlayer = player;
                    }
                }
            }
            return myRoomPlayer;
        }
    }

    public GameObject ruleSettingButton;

    public void Open()
    {
        MyRoomPlayer.lobbyPlayer.isMoving = false;
        ruleSettingButton.SetActive(false);
    }

    public void Close()
    {
        MyRoomPlayer.lobbyPlayer.isMoving = true;
        ruleSettingButton.SetActive(true);

    }
    [SyncVar]
    public string playerName;
    


    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        LobbyUI.instance.PlayerCounter.UpdatePlayerCount();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        SpawnPlayerInLobby();
        LobbyUI.instance.ActivateButton();

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        CmdSetPlayerName(PlayerSettings.nickname);
        Debug.Log("Name: " + PlayerSettings.nickname);

    }


    private void OnDestroy()
    {
        LobbyUI.instance.PlayerCounter.UpdatePlayerCount();
    }

    [Command]
    public void CmdSetPlayerName(string name)
    {
        playerName = name;
        lobbyPlayer.playerName = name;
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

        playerChar.playerColor = color;
    }
}