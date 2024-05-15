using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Command : MonoBehaviour
{
    [SerializeField] InfoPanel infoPanel;
    private void Start()
    {

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
                infoPanel.ShowPlayerName();
                break;
            default:
                break;
        }
    }
}
