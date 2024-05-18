using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SynVariable : MonoBehaviourPunCallbacks
{
    public static SynVariable instance;
    // RPCメソッドの引数 object[] の配列にする
    public object[] args = new object[]{
            "RPC message",          // 第1引数 : string msg
            new byte[] {1, 2, 3}    // 第2引数 : byte[] data
            };


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
    void Rpc(object [] obj)
    {
        args = obj;
    }

    /// <summary>
    /// 変数を同期
    /// </summary>
    public void RpcSendVariable()
    {
        photonView.RPC(nameof(Rpc), RpcTarget.All, args);
    }
}
