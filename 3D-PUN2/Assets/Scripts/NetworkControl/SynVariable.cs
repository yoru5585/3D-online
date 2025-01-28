using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SynVariable : MonoBehaviourPunCallbacks
{
    public static SynVariable instance;
    // RPC���\�b�h�̈��� object[] �̔z��ɂ���
    public object[] args = new object[]{
            "RPC message",          // ��1���� : string msg
            new byte[] {1, 2, 3}    // ��2���� : byte[] data
            };


    private void Awake()
    {
        //���g���d�����Ă��邩�`�F�b�N
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [PunRPC]
    void Rpc(object [] obj)
    {
        args = obj;
    }

    /// <summary>
    /// �ϐ��𓯊�
    /// </summary>
    public void RpcSendVariable()
    {
        if (PhotonNetwork.OfflineMode)
        {
            return;
        }
        photonView.RPC(nameof(Rpc), RpcTarget.All, args);
    }
}
