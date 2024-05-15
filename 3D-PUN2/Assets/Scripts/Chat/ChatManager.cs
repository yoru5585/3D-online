using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;
using TMPro;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    ChatClient chatClient;
    ChatLog chatLog;
    Command command;
    string username;
    string channel;
    bool isConnected;
    string currentChat;
    string privateReceiver = "";
    [SerializeField] TMP_InputField chatField;
    [SerializeField] TextMeshProUGUI chatDisplay;
    [SerializeField] Button sendButton;
    [SerializeField] GameObject waitText;

    private void Start()
    {
        chatLog = GetComponent<ChatLog>();
        command = GetComponent<Command>();
    }

    public void SetUserName(string username)
    {
        this.username = username;
    }

    public void SetChannel(string channel)
    {
        this.channel = channel;
    }

    /// <summary>
    /// チャットを開始するときに実行する
    /// </summary>
    /// <param name="sceneName">移動するシーンの名前</param>
    public void ChatStart()
    {
        waitText.SetActive(true);
        sendButton.interactable = false;
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "ASIA";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>チャットサーバーに接続しています......");
    }

    public void SubToChat()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>チャンネルに参加しました。");
        chatClient.Subscribe(new string[] { channel});
        //自分がマスタークライアントなら
        if (PhotonNetwork.IsMasterClient)
        {
            //カスタムプロパティを設定
            chatLog.SetupProperty();
        }
        //マスタークライアントではない
        else
        {
            //プロパティから過去のチャットログを取得
            chatDisplay.text = chatLog.GetChatlog();
        }
    }

    public void TypePublicChatOnValueChange(string ChatIn)
    {
        currentChat = ChatIn;
    }

    public void SubmitPublicChat()
    {
        if (privateReceiver == "")
        {
            chatClient.PublishMessage(channel, currentChat);
            command.OnChatSubmitted(currentChat);
            chatField.text = "";
            currentChat = "";
            Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>全員にメッセージを送りました。");
        }
    }

    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }

    public void SubmitPrivateChat()
    {
        if (privateReceiver != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
            Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>{privateReceiver}にメッセージを送りました。");
        }
    }

    public void SendSystemLog(string log)
    {
        chatClient.PublishMessage(channel, log);
        chatField.text = "";
        currentChat = "";
    }

    public string GetChatDisplay()
    {
        return chatDisplay.text;
    }

    private void Update()
    {
        if (!isConnected)
        {
            return;
        }

        chatClient.Service();
    }

    public void ClearChatDisplay()
    {
        chatDisplay.text = "";
        //チャンネルから移動
        chatClient.Unsubscribe(new string[] { channel });
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>チャットサーバーに接続しました。");
        sendButton.interactable = true;
        waitText.SetActive(false);
        SubToChat();
    }

    public void OnDisconnected()
    {
        
    }

    //メッセージを取得したとき
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>メッセージを受信しました。");
        for (int i = 0; i < senders.Length; i++)
        {
            if (messages[i].ToString().Contains("/"))
            {
                command.OnCommandReceived(messages[i].ToString());
            }
            chatDisplay.text += $"{senders[i]}:\n{messages[i]}\n";

            //自分がマスタークライアントならバッファにメッセージを保管しておく
            if (PhotonNetwork.IsMasterClient)
            {
                chatLog.AddChatTextBuffer($"{senders[i]}:\n{messages[i]}\n");
            }
        }
        
    }

    //プライベートメッセージを取得したとき
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>プライベートメッセージを受信しました。");
        chatDisplay.text += $"<color=#{0xFF0000FF:X}>{sender}</color>:\n{message}\n";
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    //自分がチャンネルに参加したとき
    public void OnSubscribed(string[] channels, bool[] results)
    {
        
    }

    public void OnUnsubscribed(string[] channels)
    {

    }
    //他のユーザーがルームに参加したとき（本来はチャットチャンネルに参加したとき）
    public void OnUserSubscribed(string channel, string user)
    {
        //なぜかコールバックされない
        //gameLauncherから実行させている
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>{user} が参加しました。"); 
        chatDisplay.text += $"<color=#{0x2A48F5FF:X}>【システム】</color><color=#{0x13FC03FF:X}>{user}</color><color=#{0x2A48F5FF:X}> さんが参加しました。\n</color>";
        //自分がマスタークライアントならルームプロパティを更新する
        if (PhotonNetwork.IsMasterClient)
        {
            chatLog.AddChatTextBuffer($"<color=#{0x2A48F5FF:X}>【システム】</color><color=#{0x13FC03FF:X}>{user}</color><color=#{0x2A48F5FF:X}> さんが参加しました。\n</color>");
            chatLog.SetChatlog();
        }
    }
    //他のユーザーがルームから退出したとき（本来はチャットチャンネルから退出したとき）
    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>【NetworkInfo】</color>{user} が退出しました。");
        chatDisplay.text += $"<color=#{0x2A48F5FF:X}>【システム】</color><color=#{0x13FC03FF:X}>{user}</color><color=#{0x2A48F5FF:X}> さんが退出しました。\n</color>";
    }

}