using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class GameLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerInfo_s playerInfo;

    [SerializeField] GameObject myPlayerAvatar;

    [SerializeField] ChatManager chatManager;

    [SerializeField] GameObject loadingImg;
    private void Awake()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        loadingImg.SetActive(true);
    }

    private void Start()
    {
        // 未設定の場合ランダムなプレイヤー名を設定する
        if (playerInfo.playerName == "")
        {
            playerInfo.playerName = $"プレイヤー{Random.Range(0, 10)}";
        }
        PhotonNetwork.NickName = playerInfo.playerName;
    }

    public void JoinRoom(string roomName)
    {
        loadingImg.SetActive(true);
        // "Room"という名前のルームに参加する
        bool isSuccess = PhotonNetwork.JoinRoom(roomName);
        GetComponent<InfoPanel>().ShowRoomName(roomName);
        chatManager.SetUserName(playerInfo.playerName);
        chatManager.SetChannel(roomName);

        if (isSuccess)
        {
            Debug.Log(roomName + "：部屋に入りました");
        }
        else
        {
            Debug.Log("部屋に入るのに失敗しました");
        }

    }

    public void CreateRoom(string roomName, int playerNum)
    {
        loadingImg.SetActive(true);
        // ルームの参加人数を4人に設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = playerNum;
        bool isSuccess = PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        GetComponent<InfoPanel>().ShowRoomName(roomName);
        chatManager.SetUserName(playerInfo.playerName);
        chatManager.SetChannel(roomName);

        if (isSuccess)
        {
            Debug.Log(roomName + "：部屋を作りました");
        }
        else
        {
            Debug.Log("部屋をつくるのに失敗しました");
        }
    }

    public void LeftRoom()
    {
        //部屋から抜ける
        Debug.Log("部屋から抜けました");
        Destroy(myPlayerAvatar);
        PhotonNetwork.LeaveRoom();
        chatManager.ClearChatDisplay();

    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーへの接続に成功しました。");
        //ロビーに参加する
        PhotonNetwork.JoinLobby();
        loadingImg.SetActive(false);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(0, 1, 0);
        myPlayerAvatar = PhotonNetwork.Instantiate("PlayerAvatar", position, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("your masterclient");
            // PhotonNetwork.ServerTimestamp を使って現在のタイムスタンプを取得
            ExitGames.Client.Photon.Hashtable startTimeProps = new ExitGames.Client.Photon.Hashtable();
            startTimeProps["StartTime"] = PhotonNetwork.ServerTimestamp;

            // ルームのカスタムプロパティとして開始時刻を設定
            PhotonNetwork.CurrentRoom.SetCustomProperties(startTimeProps);
        }

        GetComponent<InfoPanel>().InfoPanelSetup();
        GetComponent<InfoPanel>().ShowPlayerName();
        GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);
        loadingImg.SetActive(false);

    }

    // 他プレイヤーがルームへ参加した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName}が参加しました");
        GetComponent<InfoPanel>().ShowPlayerName();
        GetComponent<InfoPanel>().ShowPlayerNum();
        GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);

        chatManager.OnUserSubscribed("", newPlayer.NickName);

    }

    // 他プレイヤーがルームから退出した時に呼ばれるコールバック
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName}が退出しました");
        GetComponent<InfoPanel>().ShowPlayerName();
        GetComponent<InfoPanel>().ShowPlayerNum();
        GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);

        chatManager.OnUserUnsubscribed("", otherPlayer.NickName);

    }

    //ルームが更新されたとき
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("ルームが更新されました。");
        GetComponent<RoomListManager>().SetRoomList(roomList);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("ロビーに参加しました。");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("ロビーから抜けました。");
    }
}