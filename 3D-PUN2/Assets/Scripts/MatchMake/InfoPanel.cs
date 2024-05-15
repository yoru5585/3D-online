using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;


public class InfoPanel : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> playerName;

    [SerializeField] TextMeshProUGUI roomName;

    [SerializeField] TextMeshProUGUI maxPlayerNum;

    [SerializeField] GameObject namePrefab;

    [SerializeField] Transform namePrefabParent;

    [SerializeField] Button startButton;

    public void ShowPlayerName()
    {
        foreach (TextMeshProUGUI text in playerName)
        {
            text.text = ".......";
            text.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        }

        int index = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == PhotonNetwork.LocalPlayer.NickName)
            {
                playerName[index].text = $"<color=#{0x13FC03FF:X}>{player.NickName}</color>";
            }
            else
            {
                playerName[index].text = player.NickName;
            }

            if (player.IsMasterClient)
            {
                playerName[index].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                playerName[index].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

            index++;
        }
    }

    public void ShowRoomName(string roomName)
    {
        this.roomName.text = roomName;
    }

    public void InfoPanelSetup()
    {
        //èâä˙âª
        foreach (var tmp in playerName)
        {
            Destroy(tmp.gameObject);
        }
        playerName.Clear();

        //í«â¡
        for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            var obj = Instantiate(namePrefab, namePrefabParent);
            playerName.Add(obj.GetComponent<TextMeshProUGUI>());
        }
        ShowPlayerNum();
    }

    public void ShowPlayerNum()
    {
        maxPlayerNum.text = PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public void InteractableStartButton(bool isMasterClient)
    {
        //Debug.Log("isMasterClient:"+isMasterClient);
        //startButton.interactable = isMasterClient;
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            startButton.interactable = isMasterClient;
        }

    }
}
