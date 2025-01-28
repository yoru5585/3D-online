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
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>�T�[�o�[����ؒf���܂����B");
        //SceneManager.LoadScene("TitleScene");
        FadeManager.Instance.LoadScene("TitleScene", 1f);
    }

    /// <summary>
    /// �T�[�o�[����ڑ���؂��ăQ�[�����I������
    /// </summary>
    public void RpcEndGame()
    {
        if (PhotonNetwork.OfflineMode)
        {
            Camera.main.transform.parent = null;
            PhotonNetwork.Disconnect();
            Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>�T�[�o�[����ؒf���܂����B");
            FadeManager.Instance.LoadScene("TitleScene", 1f);
        }
        else
        {
            photonView.RPC(nameof(Rpc), RpcTarget.All);
        }
        
    }
}
