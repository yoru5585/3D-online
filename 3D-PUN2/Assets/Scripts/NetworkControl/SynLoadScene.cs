using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SynLoadScene : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// シーン移動するときはこれを使う。
    /// マスタークライアントしか実行できない。
    /// </summary>
    /// <param name="sceneName">移動するシーンの名前</param>
    public void LoadScene(string sceneName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}
