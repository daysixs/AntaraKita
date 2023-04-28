using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    public static LobbyUI instance;

    public void Awake()
    {
        instance = this;
    }

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button ruleButton;

    [SerializeField]
    private PlayerCounter playerCounter;
    public PlayerCounter PlayerCounter { get { return playerCounter; } }

    public void ActivateButton()
    {
        startButton.gameObject.SetActive(true);
        ruleButton.gameObject.SetActive(true);

    }

    public void SetIntertract(bool isInteractable)
    {
        startButton.interactable = isInteractable;
    }

    public void OnClickStartButton()
    {
        var manager = NetworkManager.singleton as RoomManager;
        manager.ruleData = FindObjectOfType<RuleManager>().GetRuleData();
        var players = FindObjectsOfType<RoomPlayer>();
        for(int i=0;i<players.Length;i++)
        {
            players[i].readyToBegin = true;
        }
        manager.ServerChangeScene(manager.GameplayScene);
    }
    public void ExitGameRoom()
    {
        var manager = RoomManager.singleton;
        if (manager.mode == NetworkManagerMode.Host)
        {
            manager.StopHost();
        }
        else if (manager.mode == NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        }
    }
}