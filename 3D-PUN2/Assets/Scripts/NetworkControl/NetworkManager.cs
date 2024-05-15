using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    SynVariableStorage myStorage;

    private void Awake()
    {
        //自身が重複しているかチェック
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        myStorage = new SynVariableStorage();
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
            PhotonNetwork.LoadLevel("BattleScene");
        }
    }

    /// <summary>
    /// 時間管理したいときはここから取得する。
    /// ローカル時間だと環境によってズレるらしい。
    /// </summary>
    public float GetServerTime()
    {
        int currentTime = PhotonNetwork.ServerTimestamp;
        return currentTime;
    }

    /// <summary>
    /// 同期する変数をまとめたクラスを取得
    /// </summary>
    public SynVariableStorage GetSynVariable() 
    { 
        return myStorage; 
    }

    [PunRPC]
    void Rpc(SynVariableStorage storage)
    {
        myStorage = storage;
        Debug.Log(myStorage.SampleString);
    }

    /// <summary>
    /// 変数を同期
    /// </summary>
    public void RpcSendVariable()
    {
        photonView.RPC(nameof(Rpc), RpcTarget.All, myStorage);
    }
}
