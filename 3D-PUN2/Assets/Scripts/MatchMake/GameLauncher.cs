using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class GameLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerInfo_s playerInfo;

    [SerializeField] GameObject loadingImg;

    [SerializeField] Button gameStartButton;

    [HideInInspector] public ChatManager chatManager;

    [HideInInspector] public InfoPanel infoPanel;

    GameObject myPlayerAvatar;

    private void Awake()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        //シーン移動に必要？
        PhotonNetwork.AutomaticallySyncScene = true;
        loadingImg.SetActive(true);
    }

    private void Start()
    {
        chatManager = GameObject.FindGameObjectWithTag("ChatPanel").GetComponent<ChatManager>();
        infoPanel = GameObject.FindGameObjectWithTag("RoomInfoPanel").GetComponent<InfoPanel>();

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
        //ルームに参加する
        bool isSuccess = PhotonNetwork.JoinRoom(roomName);

        if (isSuccess)
        {
            Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>{roomName}：部屋に入りました");
            infoPanel.ShowRoomName(roomName);
            chatManager.SetUserName(playerInfo.playerName);
            chatManager.SetChannel(roomName);
            //チャットを開始する
            chatManager.ChatStart();
        }
        else
        {
            Debug.Log("部屋に入るのに失敗しました");
        }

    }

    public void CreateRoom(string roomName, int playerNum)
    {
        loadingImg.SetActive(true);
        // ルームの参加人数を設定する
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = playerNum;
        bool isSuccess = PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);

        if (isSuccess)
        {
            Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>部屋を作りました。：{roomName}");
            infoPanel.ShowRoomName(roomName);
            chatManager.SetUserName(playerInfo.playerName);
            chatManager.SetChannel(roomName);
            //チャットを開始する
            chatManager.ChatStart();
        }
        else
        {
            Debug.Log("部屋をつくるのに失敗しました");
        }
    }

    void InteractableStartButton(bool isMasterClient)
    {
        //Debug.Log("isMasterClient:"+isMasterClient);
        //startButton.interactable = isMasterClient;
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            gameStartButton.interactable = isMasterClient;
        }

    }

    public void LeftRoom()
    {
        //部屋から抜ける
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>部屋から退出しました。");
        Destroy(myPlayerAvatar);
        PhotonNetwork.LeaveRoom();
        chatManager.ClearChatDisplay();

    }

    //マスタークライアントが変更されたときに呼ばれるコールバック
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        InteractableStartButton(PhotonNetwork.IsMasterClient);
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>マスターサーバーへの接続に成功しました。");
        //ロビーに参加する
        PhotonNetwork.JoinLobby();
        loadingImg.SetActive(false);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(0, 1, 10);
        myPlayerAvatar = PhotonNetwork.Instantiate("PlayerAvatar", position, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>あなたがマスタークライアントです。");
            // PhotonNetwork.ServerTimestamp を使って現在のタイムスタンプを取得
            ExitGames.Client.Photon.Hashtable startTimeProps = new ExitGames.Client.Photon.Hashtable();
            startTimeProps["StartTime"] = PhotonNetwork.ServerTimestamp;

            // ルームのカスタムプロパティとして開始時刻を設定
            PhotonNetwork.CurrentRoom.SetCustomProperties(startTimeProps);
            //networkmanagerも生成する
            PhotonNetwork.Instantiate("NetworkController", Vector3.zero, Quaternion.identity);
        }

        infoPanel.InfoPanelSetup();
        infoPanel.ShowPlayerName();
        InteractableStartButton(PhotonNetwork.IsMasterClient);
        loadingImg.SetActive(false);

    }

    // 他プレイヤーがルームへ参加した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>{newPlayer.NickName}が参加しました。");
        infoPanel.ShowPlayerName();
        infoPanel.ShowPlayerNum();
        InteractableStartButton(PhotonNetwork.IsMasterClient);

        chatManager.OnUserSubscribed("", newPlayer.NickName);

    }

    // 他プレイヤーがルームから退出した時に呼ばれるコールバック
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>{otherPlayer.NickName}が退出しました。");
        infoPanel.ShowPlayerName();
        infoPanel.ShowPlayerNum();
        InteractableStartButton(PhotonNetwork.IsMasterClient);

        chatManager.OnUserUnsubscribed("", otherPlayer.NickName);

    }

    //ルームが更新されたとき
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>ルームが更新されました。");
        GetComponent<RoomListManager>().SetRoomList(roomList);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>ロビーに参加しました。");
    }

    public override void OnLeftLobby()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>ロビーから抜けました。");
    }
}