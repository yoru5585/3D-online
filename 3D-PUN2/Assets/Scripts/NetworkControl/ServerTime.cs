using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public static class ServerTime
{
    /// <summary>
    /// ���ԊǗ��������Ƃ��͂�������擾����B
    /// ���[�J�����Ԃ��Ɗ��ɂ���ăY����炵���B
    /// </summary>
    public static float GetServerTime()
    {
        float currentTime = PhotonNetwork.ServerTimestamp;
        return currentTime;
    }
}
