using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Command : MonoBehaviour
{
    GameObject Scripts;
    private void Start()
    {
        Scripts = GameObject.Find("Scripts");
    }
    public void OnChatSubmitted(string mess)
    {
        switch (mess)
        {
            case "/MasterClient Request":
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                GetComponent<ChatManager>().SendSystemLog(
                    $"<color=#{0x2A48F5FF:X}>【システム】{PhotonNetwork.LocalPlayer.NickName} さんがマスタークライアントになりました。</color>"
                    );
                break;
            default:
                break;
        }
    }

    public void OnCommandReceived(string mess)
    {
        switch (mess)
        {
            case "/MasterClient Request":
                Scripts.GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);
                Scripts.GetComponent<InfoPanel>().ShowPlayerName();
                break;
            default:
                break;
        }
    }
}
