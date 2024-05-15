using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerTime : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// 時間管理したいときはここから取得する。
    /// ローカル時間だと環境によってズレるらしい。
    /// </summary>
    public float GetServerTime()
    {
        int currentTime = PhotonNetwork.ServerTimestamp;
        return currentTime;
    }
}
