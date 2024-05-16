using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class NetworkDisconnect : MonoBehaviourPunCallbacks
{

    [PunRPC]
    void Rpc()
    {
        Camera.main.transform.parent = null;
        PhotonNetwork.Disconnect();
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>サーバーから切断しました。");
        //SceneManager.LoadScene("TitleScene");
        FadeManager.Instance.LoadScene("TitleScene", 1f);
    }

    /// <summary>
    /// サーバーから接続を切ってゲームを終了する
    /// </summary>
    public void RpcEndGame()
    {
        photonView.RPC(nameof(Rpc), RpcTarget.All);
    }
}
