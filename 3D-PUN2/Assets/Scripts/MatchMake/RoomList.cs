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
    int joinMaxNum = 0;
    string roomName;

    void Update()
    {
        if (joinNum == joinMaxNum)
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

    public void SetInfo(string roomName, string joinNum, string roomMaxNum)
    {
        roomNameText.text = roomName;
        joinNumText.text = joinNum + " / " + roomMaxNum;
        this.roomName = roomName;
        this.joinNum = int.Parse(joinNum);
        this.joinMaxNum = int.Parse(roomMaxNum);
    }

    public string GetRoomName()
    {
        return roomName;
    }
}
