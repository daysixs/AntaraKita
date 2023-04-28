using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum EPlayerType
{
    Crew,
    Imposter
}

public class InGamePlayerMovement : PlayerMovement
{
    [SyncVar]
    public EPlayerType playerType;

    public override void Start()
    {
        base.Start();

        if (isOwned)
        {
            isMoving = true;
            CmdSetPlayerCharacter(PlayerSettings.nickname);
        }

        //GameSystem.instance.AddPlayer(this);
    }

    [Command]
    public void CmdSetPlayerCharacter(string name)
    {
        this.playerName = name;
    }
}
