using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public EPlayerColor playerColor;

    private void Start()
    {
        base.Start();

        if (isServer)
        {
            SpawnPlayerInLobby();
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

        var spawnPositions = FindObjectOfType<SpawnPositions>();

        int index = spawnPositions.Index;

        Vector3 spawnPos = spawnPositions.GetSpawnPos();

        var playerChar = Instantiate(RoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyPlayerRestrict>();

        playerChar.transform.localScale = index < 5 ? new Vector3(0.5f, 0.5f, 1f) : new Vector3(-0.5f, 0.5f, 1f);

        NetworkServer.Spawn(playerChar.gameObject, connectionToClient);

        playerChar.playerColor = color;
    }
}