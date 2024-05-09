using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class InfoPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI [] playerName;

    [SerializeField] TextMeshProUGUI roomName;
    public void ShowPlayerName()
    {
        foreach (TextMeshProUGUI text in playerName)
        {
            text.text = ".......";
        }

        int index = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            playerName[index].text = player.NickName;
            index++;
        }
    }

    public void ShowRoomName(string roomName)
    {
        this.roomName.text = roomName;
    }
}
