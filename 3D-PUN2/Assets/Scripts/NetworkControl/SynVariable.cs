using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SynVariable : MonoBehaviourPunCallbacks
{
    public static SynVariable instance;
    VariableStorage myStorage;

    private void Awake()
    {
        //自身が重複しているかチェック
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        myStorage = new VariableStorage();
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

    /// <summary>
    /// 同期する変数をまとめたクラスを取得
    /// </summary>
    public VariableStorage GetSynVariable()
    {
        return myStorage;
    }

    [PunRPC]
    void Rpc(VariableStorage storage)
    {
        myStorage = storage;
        Debug.Log(myStorage.SampleShortInt);
    }

    /// <summary>
    /// 変数を同期
    /// </summary>
    public void RpcSendVariable()
    {
        photonView.RPC(nameof(Rpc), RpcTarget.All, myStorage);
    }
}
