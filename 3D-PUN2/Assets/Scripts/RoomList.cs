using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomList : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI roomNameText;

    [SerializeField]
    TextMeshProUGUI joinNumText;

    [SerializeField]
    Button joinButton;

    int joinNum = 0;
    string roomName;

    void Update()
    {
        if (joinNum == 4)
        {
            joinNumText.color = Color.red;
            joinButton.interactable = false;
        }
        else
        {
            joinNumText.color = Color.black;
            joinButton.interactable = true;
        }
    }

    public void SetInfo(string roomName, string joinNum)
    {
        roomNameText.text = roomName;
        joinNumText.text = joinNum + " / 4";
        this.roomName = roomName;
        this.joinNum = int.Parse(joinNum);
    }

    public string GetRoomName()
    {
        return roomName;
    }
}
