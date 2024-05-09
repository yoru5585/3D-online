using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using TMPro;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    ChatClient chatClient;
    string username;
    string channel;
    bool isConnected;
    string currentChat;
    string privateReceiver = "";
    [SerializeField] TMP_InputField chatField;
    [SerializeField] TextMeshProUGUI chatDesplay;
    [SerializeField] Button sendButton;
    [SerializeField] GameObject waitText;

    private void OnEnable()
    {
        waitText.SetActive(true);
        ChatStart();
    }

    public void SetUserName(string username)
    {
        this.username = username;
    }

    public void SetChannel(string channel)
    {
        this.channel = channel;
    }

    public void ChatStart()
    {
        isConnected = true;
        sendButton.interactable = false;
        chatClient = new ChatClient(this);
        //chatClient.ChatRegion = "jp";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new Photon.Chat.AuthenticationValues(username));
        Debug.Log("Connectiong");
    }

    public void SubToChat()
    {
        Debug.Log("チャンネルに参加しました");
        chatClient.Subscribe(new string[] { channel});
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
            chatField.text = "";
            currentChat = "";
            Debug.Log("全員にメッセージを送りました");
        }
    }

    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }

    public void SumitPrivateChat()
    {
        if (privateReceiver != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
            Debug.Log(privateReceiver + "にメッセージを送りました");
        }
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
        chatDesplay.text = "";
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
        Debug.Log("Connected");
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
        Debug.Log("メッセージを受信しました");
        for (int i = 0; i < senders.Length; i++)
        {
            chatDesplay.text += $"{senders[i]}: {messages[i]}\n";
        }
    }

    //プライベートメッセージを取得したとき
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log("プライベートメッセージを受信しました");
        chatDesplay.text += $"<color=#{0xFF0000FF:X}>{sender}</color>: {message}\n";
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        
    }

    public void OnUnsubscribed(string[] channels)
    {

    }
    //他のユーザーが参加したとき
    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log("なぜか実行されない");
        chatDesplay.text += $"<color=#{0x13FC03FF:X}>{user}</color> が参加しました。\n";
    }
    //他のユーザーが退会したとき
    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log(user + "が退出しました。");
        chatDesplay.text += $"<color=#{0x13FC03FF:X}>{user}</color> が退出しました。\n";
    }

}