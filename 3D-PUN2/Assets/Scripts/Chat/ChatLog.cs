using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;

public class ChatLog : MonoBehaviourPunCallbacks, IPunObservable
{
    string textlog;

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(textlog);
        }
        else
        {
            textlog = stream.ReceiveNext().ToString();
        }
    }

    public void SetTextlog(string textlog)
    {
        this.textlog = textlog;
    }
}
