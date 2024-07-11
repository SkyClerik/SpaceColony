﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

namespace Photon.Chat.Demo
{
    public class ChatGui : MonoBehaviour, IChatClientListener
    {
        public string[] ChannelsToJoinOnConnect; // set in inspector. Demo channels to join automatically.
        public string[] FriendsList;
        public int HistoryLengthToFetch; // set in inspector. Up to a certain degree, previously sent messages can be fetched for context
        public string UserName { get; set; }
        private string selectedChannelName; // mainly used for GUI/input
        public ChatClient chatClient;
        protected internal ChatAppSettings chatAppSettings;
        public GameObject missingAppIdErrorPanel;
        public GameObject ConnectingLabel;
        public RectTransform ChatPanel;     // set in inspector (to enable/disable panel)
        public GameObject UserIdFormPanel;
        public InputField InputFieldChat;   // set in inspector
        public Text CurrentChannelText;     // set in inspector
        public Toggle ChannelToggleToInstantiate; // set in inspector
        public GameObject FriendListUiItemtoInstantiate;
        private readonly Dictionary<string, Toggle> channelToggles = new Dictionary<string, Toggle>();
        private readonly Dictionary<string, FriendItem> friendListItemLUT = new Dictionary<string, FriendItem>();
        public bool ShowState = true;
        public GameObject Title;
        public Text StateText; // set in inspector
        public Text UserIdText; // set in inspector
        #region HelpText
        private static string HelpText = "\n    -- HELP --\n" +
            "To subscribe to channel(s) (channelnames are case sensitive) :  \n" +
                "\t<color=#E07B00>\\subscribe</color> <color=green><list of channelnames></color>\n" +
                "\tor\n" +
                "\t<color=#E07B00>\\s</color> <color=green><list of channelnames></color>\n" +
                "\n" +
                "To leave channel(s):\n" +
                "\t<color=#E07B00>\\unsubscribe</color> <color=green><list of channelnames></color>\n" +
                "\tor\n" +
                "\t<color=#E07B00>\\u</color> <color=green><list of channelnames></color>\n" +
                "\n" +
                "To switch the active channel\n" +
                "\t<color=#E07B00>\\join</color> <color=green><channelname></color>\n" +
                "\tor\n" +
                "\t<color=#E07B00>\\j</color> <color=green><channelname></color>\n" +
                "\n" +
                "To send a private message: (username are case sensitive)\n" +
                "\t\\<color=#E07B00>msg</color> <color=green><username></color> <color=green><message></color>\n" +
                "\n" +
                "To change status:\n" +
                "\t\\<color=#E07B00>state</color> <color=green><stateIndex></color> <color=green><message></color>\n" +
                "<color=green>0</color> = Offline " +
                "<color=green>1</color> = Invisible " +
                "<color=green>2</color> = Online " +
                "<color=green>3</color> = Away \n" +
                "<color=green>4</color> = Do not disturb " +
                "<color=green>5</color> = Looking For Group " +
                "<color=green>6</color> = Playing" +
                "\n\n" +
                "To clear the current chat tab (private chats get closed):\n" +
                "\t<color=#E07B00>\\clear</color>";
        #endregion

        public void Start()
        {
            DontDestroyOnLoad(this.gameObject);

            this.UserIdText.text = "";
            this.StateText.text = "";
            this.StateText.gameObject.SetActive(true);
            //this.UserIdText.gameObject.SetActive(true);
            this.Title.SetActive(true);
            this.ChatPanel.gameObject.SetActive(false);
            this.ConnectingLabel.SetActive(false);

            if (string.IsNullOrEmpty(this.UserName))
                this.UserName = "user" + Environment.TickCount % 99; //made-up username

            this.chatAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();

            bool appIdPresent = !string.IsNullOrEmpty(this.chatAppSettings.AppIdChat);

            this.missingAppIdErrorPanel.SetActive(!appIdPresent);
            //this.UserIdFormPanel.gameObject.SetActive(appIdPresent);

            if (!appIdPresent)
                Debug.LogError("You need to set the chat app ID in the PhotonServerSettings file in order to continue.");
        }

        public void Connect()
        {
            enabled = true;
            Start();

            //this.UserIdFormPanel.gameObject.SetActive(false);

            this.chatClient = new ChatClient(this);

            this.chatClient.AuthValues = new AuthenticationValues(this.UserName);
            this.chatClient.ConnectUsingSettings(this.chatAppSettings);

            this.ChannelToggleToInstantiate.gameObject.SetActive(false);
            Debug.Log("Connecting as: " + this.UserName);

            this.ConnectingLabel.SetActive(true);
        }

        /// <summary>To avoid that the Editor becomes unresponsive, disconnect all Photon connections in OnDestroy.</summary>
        public void OnDestroy()
        {
            if (this.chatClient != null)
                this.chatClient.Disconnect();
        }

        /// <summary>To avoid that the Editor becomes unresponsive, disconnect all Photon connections in OnApplicationQuit.</summary>
        public void OnApplicationQuit()
        {
            if (this.chatClient != null)
                this.chatClient.Disconnect();
        }

        public void Update()
        {
            if (this.chatClient != null)
                this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!

            // check if we are missing context, which means we got kicked out to get back to the Photon Demo hub.
            if (this.StateText == null)
            {
                Destroy(this.gameObject);
                return;
            }

            this.StateText.gameObject.SetActive(this.ShowState); // this could be handled more elegantly, but for the demo it's ok.
        }


        public void OnEnterSend()
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            {
                this.SendChatMessage(this.InputFieldChat.text);
                this.InputFieldChat.text = "";
            }
        }

        public void OnClickSend()
        {
            if (this.InputFieldChat != null)
            {
                this.SendChatMessage(this.InputFieldChat.text);
                this.InputFieldChat.text = "";
            }
        }

        public int TestLength = 2048;
        private byte[] testBytes = new byte[2048];

        private void SendChatMessage(string inputLine)
        {
            if (string.IsNullOrEmpty(inputLine))
                return;

            this.chatClient.PublishMessage(this.selectedChannelName, inputLine);
        }

        public void PostHelpToCurrentChannel()
        {
            this.CurrentChannelText.text += HelpText;
        }

        public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
        {
            if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
            {
                Debug.LogError(message);
            }
            else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
            {
                Debug.LogWarning(message);
            }
            else
            {
                Debug.Log(message);
            }
        }

        public void OnConnected()
        {
            if (this.ChannelsToJoinOnConnect != null && this.ChannelsToJoinOnConnect.Length > 0)
            {
                this.chatClient.Subscribe(this.ChannelsToJoinOnConnect, this.HistoryLengthToFetch);
            }

            this.ConnectingLabel.SetActive(false);

            this.UserIdText.text = "Connected as " + this.UserName;

            this.ChatPanel.gameObject.SetActive(true);

            if (this.FriendsList != null && this.FriendsList.Length > 0)
            {
                this.chatClient.AddFriends(this.FriendsList); // Add some users to the server-list to get their status updates

                // add to the UI as well
                foreach (string _friend in this.FriendsList)
                {
                    if (this.FriendListUiItemtoInstantiate != null && _friend != this.UserName)
                    {
                        this.InstantiateFriendButton(_friend);
                    }

                }

            }

            if (this.FriendListUiItemtoInstantiate != null)
                this.FriendListUiItemtoInstantiate.SetActive(false);

            this.chatClient.SetOnlineStatus(ChatUserStatus.Online); // You can set your online state (without a mesage).
        }

        public void OnDisconnected()
        {
            Debug.Log("OnDisconnected()");
            this.ConnectingLabel.SetActive(false);
        }

        public void OnChatStateChange(ChatState state)
        {
            this.StateText.text = state.ToString();
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            // в этой демонстрации мы просто отправляем сообщение в каждый канал. Это не обязательно!
            foreach (string channel in channels)
            {
                this.chatClient.PublishMessage(channel, "says 'hi'."); // вам не обязательно отправлять сообщение о присоединении, но вы могли бы это сделать.

                if (this.ChannelToggleToInstantiate != null)
                    this.InstantiateChannelButton(channel);
            }

            Debug.Log("OnSubscribed: " + string.Join(", ", channels));
            this.ShowChannel(channels[0]);
        }

        public void OnSubscribed(string channel, string[] users, Dictionary<object, object> properties)
        {
            Debug.LogFormat("OnSubscribed: {0}, users.Count: {1} Channel-props: {2}.", channel, users.Length, properties.ToStringFull());
        }

        private void InstantiateChannelButton(string channelName)
        {
            if (this.channelToggles.ContainsKey(channelName))
            {
                Debug.Log("Skipping creation for an existing channel toggle.");
                return;
            }

            Toggle cbtn = (Toggle)Instantiate(this.ChannelToggleToInstantiate);
            cbtn.gameObject.SetActive(true);
            cbtn.GetComponentInChildren<ChannelSelector>().SetChannel(channelName);
            cbtn.transform.SetParent(this.ChannelToggleToInstantiate.transform.parent, false);

            this.channelToggles.Add(channelName, cbtn);
        }

        private void InstantiateFriendButton(string friendId)
        {
            GameObject fbtn = (GameObject)Instantiate(this.FriendListUiItemtoInstantiate);
            fbtn.gameObject.SetActive(true);
            FriendItem _friendItem = fbtn.GetComponent<FriendItem>();

            _friendItem.FriendId = friendId;

            fbtn.transform.SetParent(this.FriendListUiItemtoInstantiate.transform.parent, false);

            this.friendListItemLUT[friendId] = _friendItem;
        }

        public void OnUnsubscribed(string[] channels)
        {
            foreach (string channelName in channels)
            {
                if (this.channelToggles.ContainsKey(channelName))
                {
                    Toggle t = this.channelToggles[channelName];
                    Destroy(t.gameObject);

                    this.channelToggles.Remove(channelName);

                    Debug.Log("Unsubscribed from channel '" + channelName + "'.");

                    // Showing another channel if the active channel is the one we unsubscribed from before
                    if (channelName == this.selectedChannelName && this.channelToggles.Count > 0)
                    {
                        IEnumerator<KeyValuePair<string, Toggle>> firstEntry = this.channelToggles.GetEnumerator();
                        firstEntry.MoveNext();

                        this.ShowChannel(firstEntry.Current.Key);

                        firstEntry.Current.Value.isOn = true;
                    }
                }
                else
                {
                    Debug.Log("Can't unsubscribe from channel '" + channelName + "' because you are currently not subscribed to it.");
                }
            }
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            if (channelName.Equals(this.selectedChannelName))
            {
                // update text
                this.ShowChannel(this.selectedChannelName);
            }
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            // as the ChatClient is buffering the messages for you, this GUI doesn't need to do anything here
            // you also get messages that you sent yourself. in that case, the channelName is determinded by the target of your msg
            // поскольку клиент чата буферизует сообщения для вас, этому графическому интерфейсу ничего не нужно делать,
            // вы также получаете сообщения, которые отправили сами. в этом случае имя канала определяется адресатом вашего сообщения
            this.InstantiateChannelButton(channelName);

            byte[] msgBytes = message as byte[];
            if (msgBytes != null)
            {
                Debug.Log("Message with byte[].Length: " + msgBytes.Length);
            }
            if (this.selectedChannelName.Equals(channelName))
            {
                this.ShowChannel(channelName);
            }

            // можно проверять тип. Но как не проверять тип? Нужен общий тип?
            if (message is UserIDForm)
            {
                
            }
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {

            Debug.LogWarning("status: " + string.Format("{0} is {1}. Msg:{2}", user, status, message));

            if (this.friendListItemLUT.ContainsKey(user))
            {
                FriendItem _friendItem = this.friendListItemLUT[user];
                if (_friendItem != null) _friendItem.OnFriendStatusUpdate(status, gotMessage, message);
            }
        }

        public void OnUserSubscribed(string channel, string user)
        {
            Debug.LogFormat("OnUserSubscribed: channel=\"{0}\" userId=\"{1}\"", channel, user);
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            Debug.LogFormat("OnUserUnsubscribed: channel=\"{0}\" userId=\"{1}\"", channel, user);
        }

        public void OnChannelPropertiesChanged(string channel, string userId, Dictionary<object, object> properties)
        {
            Debug.LogFormat("OnChannelPropertiesChanged: {0} by {1}. Props: {2}.", channel, userId, Extensions.ToStringFull(properties));
        }

        public void OnUserPropertiesChanged(string channel, string targetUserId, string senderUserId, Dictionary<object, object> properties)
        {
            Debug.LogFormat("OnUserPropertiesChanged: (channel:{0} user:{1}) by {2}. Props: {3}.", channel, targetUserId, senderUserId, Extensions.ToStringFull(properties));
        }

        public void OnErrorInfo(string channel, string error, object data)
        {
            Debug.LogFormat("OnErrorInfo for channel {0}. Error: {1} Data: {2}", channel, error, data);
        }

        public void AddMessageToSelectedChannel(string msg)
        {
            ChatChannel channel = null;
            bool found = this.chatClient.TryGetChannel(this.selectedChannelName, out channel);
            if (!found)
            {
                Debug.Log("AddMessageToSelectedChannel failed to find channel: " + this.selectedChannelName);
                return;
            }

            if (channel != null)
                channel.Add("Bot", msg, 0); //TODO: how to use msgID?
        }

        public void ShowChannel(string channelName)
        {
            if (string.IsNullOrEmpty(channelName))
            {
                return;
            }

            ChatChannel channel = null;
            bool found = this.chatClient.TryGetChannel(channelName, out channel);
            if (!found)
            {
                Debug.Log("ShowChannel failed to find channel: " + channelName);
                return;
            }

            this.selectedChannelName = channelName;
            this.CurrentChannelText.text = channel.ToStringMessages();
            Debug.Log("ShowChannel: " + this.selectedChannelName);

            foreach (KeyValuePair<string, Toggle> pair in this.channelToggles)
            {
                pair.Value.isOn = pair.Key == channelName ? true : false;
            }
        }

        public void OpenDashboard()
        {
            Application.OpenURL("https://dashboard.photonengine.com");
        }
    }
}