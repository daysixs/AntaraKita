using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameSystem : NetworkBehaviour
{
    public static GameSystem instance;

    private List<InGamePlayerMovement> players = new List<InGamePlayerMovement>();

    public void AddPlayer(InGamePlayerMovement player)
    {
        if(!players.Contains(player))
        {
            players.Add(player);
        }
    }

    private IEnumerator GameReady()
    {
        Debug.Log("GameReady!!!");
        var manager = NetworkManager.singleton as RoomManager;
        while(manager.roomSlots.Count != players.Count)
        {
            yield return null;
        }
        for(int i=0;i<manager.imposterCount;i++)
        {
            var player = players[Random.Range(0, players.Count)];
            if(player.playerType != EPlayerType.Imposter)
            {
                player.playerType = EPlayerType.Imposter;
            }
            else
            {
                i--;
            }
        }
    }

    public List<InGamePlayerMovement> GetPlayerList()
    {
        return players;
    }

    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameReady());
    }

}
