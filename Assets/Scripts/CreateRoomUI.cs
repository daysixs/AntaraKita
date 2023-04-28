using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField] private List<Image> crewImage;
    [SerializeField] private List<Button> imposterNumButton;
    [SerializeField] private List<Button> maxPlayerNumButton;

    private CreateGameRoomData roomData;

    private void Start()
    {
        for (int i = 0; i < crewImage.Count; i++)
        {
            Material matInstance = Instantiate(crewImage[i].material);
            crewImage[i].material = matInstance;
        }

        roomData = new CreateGameRoomData() { imposterNum = 1, maxPlayerNum = 10 };
        UpdateCrewImages();
    }

    public void UpdateImposterNum(int num)
    {
        roomData.imposterNum = num;

        for (int i = 0; i < imposterNumButton.Count; i++)
        {
            if (i == num - 1)

            {
                imposterNumButton[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                imposterNumButton[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        int limitMaxPlayer = num == 1 ? 4 : num == 2 ? 7 : 9;
        if (roomData.maxPlayerNum < limitMaxPlayer)
        {
            UpdateMaxPlayerNum(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerNum(roomData.maxPlayerNum);
        }

        for (int i = 0; i < maxPlayerNumButton.Count; i++)
        {
            var text = maxPlayerNumButton[i].GetComponentInChildren<TMP_Text>();
            if (i < limitMaxPlayer - 4)
            {
                maxPlayerNumButton[i].interactable = false;
                text.color = Color.gray;
            }
            else
            {
                maxPlayerNumButton[i].interactable = true;
                text.color = Color.white;
            }
        }
    }

    public void UpdateMaxPlayerNum(int num)
    {
        roomData.maxPlayerNum = num;

        for (int i = 0; i < maxPlayerNumButton.Count; i++)
        {
            if (i == num - 4)

            {
                maxPlayerNumButton[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                maxPlayerNumButton[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        UpdateCrewImages();
    }

    private void UpdateCrewImages()
    {
        for (int i = 0; i < crewImage.Count; i++)
        {
            crewImage[i].material.SetColor("_PlayerColor", Color.white);
        }

        int imposterNum = roomData.imposterNum;
        int index = 0;

        while (imposterNum != 0)
        {
            if (index >= roomData.maxPlayerNum)
            {
                index = 0;
            }

            if (crewImage[index].material.GetColor("_PlayerColor") != Color.red && Random.Range(0, 5) == 0)
            {
                crewImage[index].material.SetColor("_PlayerColor", Color.red);
                imposterNum--;
            }
            index++;
        }

        for (int i = 0; i < crewImage.Count; i++)

        {
            if (i < roomData.maxPlayerNum)
            {
                crewImage[i].gameObject.SetActive(true);
            }
            else
            {
                crewImage[i].gameObject.SetActive(false);
            }
        }
    }

    public void CreateRoom()
    {
        var manage = RoomManager.singleton;
        // opens the server and allows for clients to join the game
        manage.StartHost();
    }
}

public class CreateGameRoomData
{
    public int imposterNum;
    public int maxPlayerNum;
}