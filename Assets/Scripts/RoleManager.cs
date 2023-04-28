using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : MonoBehaviour
{

    [SerializeField]
    private GameObject crewPopUp;
    [SerializeField]
    private GameObject impoPopUp;

    public void ShowPlayerType()
    {
        var players = GameSystem.instance.GetPlayerList();

        InGamePlayerMovement myplayer = null;
        foreach (var player in players)
        {
            if (player.isOwned)
            {
                myplayer = player;
                break;
            }
        }
        if (myplayer.playerType == EPlayerType.Imposter)
        {
            impoPopUp.SetActive(true);
        }
        else
        {
            crewPopUp.SetActive(true);
        }
    }
}
