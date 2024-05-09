using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;


public class InfoPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] List<TextMeshProUGUI> playerName;

    [SerializeField] TextMeshProUGUI roomName;

    [SerializeField] TextMeshProUGUI maxPlayerNum;

    [SerializeField] GameObject namePrefab;

    [SerializeField] Button startButton;

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

    public void InfoPanelSetup()
    {
        playerName.Clear();
        playerName.Add(namePrefab.GetComponent<TextMeshProUGUI>());
        for (int i = 1; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            var obj = Instantiate(namePrefab);
            playerName.Add(obj.GetComponent<TextMeshProUGUI>());
        }
        ShowPlayerNum();
    }

    public void ShowPlayerNum()
    {
        maxPlayerNum.text = PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public void InteractableStartButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
        
    }
}
