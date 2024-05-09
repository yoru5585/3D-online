using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListManager : MonoBehaviour
{
    [SerializeField] Transform RoomParent;

    [SerializeField] GameObject RoomPrefab;

    RoomList getRoomList(string roomName)
    {
        for (int i = 0; i < RoomParent.childCount; i++)
        {
            RoomList tmp = RoomParent.GetChild(i).gameObject.GetComponent<RoomList>();
            if (tmp.GetRoomName() == roomName)
            {
                return tmp.GetComponent<RoomList>();
            }
        }
        return null;
    }
    
    public void SetRoomList(List<RoomInfo> roomList)
    {
        Debug.Log("変更されたルーム数："+roomList.Count);
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomList tmp = getRoomList(roomList[i].Name);
            if (tmp == null)
            {
                //新規作成ルーム確定
                tmp = Instantiate(RoomPrefab, RoomParent).GetComponent<RoomList>();

                var session = roomList[i];
                //ボタンにイベントを設定する
                tmp.gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GetComponent<GameLauncher>().JoinRoom(session.Name);
                    GetComponent<MatchmakingManager>().RoomJoinButton();
                });

            }

            if (!roomList[i].RemovedFromList)
            {
                Debug.Log($"ルーム内容更新: {roomList[i].Name}({roomList[i].PlayerCount}/{roomList[i].MaxPlayers})");

                var session = roomList[i];
                Debug.Log(session.Name);
                tmp.SetInfo(session.Name, session.PlayerCount.ToString());
            }
            else
            {
                Debug.Log($"ルーム削除: {roomList[i].Name}");
                Destroy(tmp.gameObject);
            }
        }
    }
}
