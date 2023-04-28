using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomManager : NetworkRoomManager
{
    public RuleData ruleData;

    public int minPlayerCount;
    public int imposterCount;

    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {

        base.OnRoomServerConnect(conn);

        var spawnPositions = FindObjectOfType<SpawnPositions>().GetSpawnPos();

        var player = Instantiate(spawnPrefabs[0], spawnPositions, Quaternion.identity);

        NetworkServer.Spawn(player, conn);
    }
}