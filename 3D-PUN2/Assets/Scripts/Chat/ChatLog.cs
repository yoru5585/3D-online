using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;

public class ChatLog : MonoBehaviourPunCallbacks
{
    ExitGames.Client.Photon.Hashtable hashtable;
    public string chatTextBuffer;

    public void AddChatTextBuffer(string mess)
    {
        //Debug.Log(mess);
        chatTextBuffer += mess;
    }
    public void SetChatlog()
    {
        hashtable["chatlog"] = chatTextBuffer;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }

    public string GetChatlog()
    {
        string chatlog = (PhotonNetwork.CurrentRoom.CustomProperties["chatlog"] is string value) ? value : "";
        //Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["chatlog"]);
        return chatlog;
    }

    public void SetupProperty()
    {
        //カスタムプロパティを設定
        hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable["chatlog"] = "";
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }
}
