using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleItem : MonoBehaviour
{
    [SerializeField]
    private GameObject ruleButton;

    // Start is called before the first frame update
    void Start()
    {
        if(!RoomPlayer.MyRoomPlayer.isServer)
        {
            ruleButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
