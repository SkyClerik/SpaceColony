using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Chat.Demo;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class ChatPanelForm : MonoBehaviour, IChatClientListener
{
    [SerializeField]
    private GameObject _loadingPage;

    private UIDocument _document;
    private VisualElement _root;

    private Label _chatTextTest;
    private const string _chatTextTestName = "ChatTextTest";
    private Button _sendButton;
    private const string _sendButtonName = "send_button";

    private TextField _inputFieldChat;
    private const string _inputFieldChatName = "InputFieldChat";

    protected internal ChatAppSettings chatAppSettings;
    private ChatClient chatClient;
    private string _selectedChannelName = "GlobalChat";

    public string UserID { get; set; }

    private void Start()
    {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;


        _chatTextTest = _root.Q<Label>(_chatTextTestName);
        _inputFieldChat = _root.Q<TextField>(_inputFieldChatName);
        _sendButton = _root.Q<Button>(_sendButtonName);
        _sendButton.clicked += SendButtonClicked;

        Hide();
        DontDestroyOnLoad(gameObject);
    }

    private void SendButtonClicked()
    {
        if (_inputFieldChat != null)
            Send();
    }

    public void OnButtonConnect()
    {
        if (string.IsNullOrEmpty(UserID))
            UserID = "user" + System.Environment.TickCount % 99;

        chatAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();

        bool appIdPresent = !string.IsNullOrEmpty(chatAppSettings.AppIdChat);
        if (!appIdPresent)
            Debug.LogError("You need to set the chat app ID in the PhotonServerSettings file in order to continue.");

        chatClient = new ChatClient(this);
        chatClient.AuthValues = new AuthenticationValues(UserID);
        chatClient.ConnectUsingSettings(chatAppSettings);
    }

    public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            Send();
    }

    private void Send()
    {
        SendChatMessage(_inputFieldChat.text);
        _inputFieldChat.value = "";
    }

    private void SendChatMessage(string inputLine)
    {
        if (string.IsNullOrEmpty(inputLine))
            return;

        //string data = JsonUtility.ToJson(_missionData);
        //chatClient.PublishMessage(_selectedChannelName, data);

        //chatClient.PublishMessage(_selectedChannelName, inputLine);
        //chatClient.SendPrivateMessage(_selectedChannelName, inputLine);
    }

    public void OnDestroy() => Disconnect();

    public void OnApplicationQuit() => Disconnect();

    private void Disconnect() => chatClient?.Disconnect();

    public void Update() => chatClient?.Service();

    private void Show() => _root.style.display = DisplayStyle.Flex;

    private void Hide() => _root.style.display = DisplayStyle.None;

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log($"DebugReturn {level} : {message}");
    }

    public void OnDisconnected()
    {
        Debug.Log($"OnDisconnected");
    }

    public void OnConnected()
    {
        _loadingPage.SetActive(false);

        _chatTextTest.text = $"Подключились к каналу '{_selectedChannelName}'";
        chatClient?.Subscribe(_selectedChannelName);
        Show();
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log($"OnChatStateChange: {state}");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        Debug.Log($"OnGetMessages: {channelName} : {senders} : {messages}");
        if (channelName.Equals(_selectedChannelName))
        {
            for (int i = 0; i < senders.Length; i++)
            {
                _chatTextTest.text += $"\n[{channelName}] {senders[i]} : {messages[i]}";

                try
                {
                    //QuestData newMD = Instantiate(_missionData);
                    //JsonUtility.FromJsonOverwrite($"{messages[i]}", newMD);
                    //Debug.Log($"{newMD.Description}");
                }
                catch (System.Exception e)
                {
                    Debug.Log($"e: {e}");
                }
            }
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log($"OnPrivateMessage: {channelName} : {message} : {channelName}");
        if (channelName.Equals(_selectedChannelName))
        {
            _chatTextTest.text += $"\n[private: {channelName}] {sender} : {message}";
        }

        byte[] msgBytes = message as byte[];
        if (msgBytes != null)
        {
            Debug.Log("Message with byte[].Length: " + msgBytes.Length);
        }
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log($"OnSubscribed: {channels} : {results}");
    }

    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log($"OnUnsubscribed: {channels}");
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log($"OnStatusUpdate: {user} : {status} : {gotMessage} : {message}");
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log($"OnUserSubscribed: {channel} : {user}");
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log($"OnUserUnsubscribed: {channel} : {user}");
    }
}