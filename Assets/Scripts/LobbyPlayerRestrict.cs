using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerRestrict : PlayerMovement
{
    public void CompleteSpawn()
    {
        if (isOwned)
        {
            isMoving = true;
        }
    }
}