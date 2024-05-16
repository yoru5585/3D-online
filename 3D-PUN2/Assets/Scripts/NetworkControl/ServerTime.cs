using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public static class ServerTime
{
    /// <summary>
    /// 時間管理したいときはここから取得する。
    /// ローカル時間だと環境によってズレるらしい。
    /// </summary>
    public static float GetServerTime()
    {
        int currentTime = PhotonNetwork.ServerTimestamp;
        return currentTime;
    }
}
