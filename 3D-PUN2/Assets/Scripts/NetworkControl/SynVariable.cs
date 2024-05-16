using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SynVariable : MonoBehaviourPunCallbacks
{
    public static SynVariable instance;
    public short SampleShortInt = 0;

    private void Awake()
    {
        //自身が重複しているかチェック
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
    void Rpc(short num)
    {
        SampleShortInt = num;
    }

    /// <summary>
    /// 変数を同期
    /// </summary>
    public void RpcSendVariable()
    {
        photonView.RPC(nameof(Rpc), RpcTarget.All, SampleShortInt);
    }
}
