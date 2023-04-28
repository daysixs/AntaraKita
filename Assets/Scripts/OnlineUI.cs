using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class OnlineUI : MonoBehaviour
{
    [SerializeField] private TMP_Text input;
    [SerializeField] private GameObject roomUI;

    public void OnClickCreateRoomButton()
    {
        if (input.text != "")
        {
            PlayerSettings.nickname = input.text;
            roomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            input.GetComponent<Animator>().SetTrigger("Shake");
        }
    }

    public void OnClickEnterLobbyButton()
    {
        if (input.text != "")
        {
            var manager = RoomManager.singleton;
            manager.StartClient();
        }
        else
        {
            input.GetComponent<Animator>().SetTrigger("Shake");
        }
    }
}