using UnityEngine;
using UnityEngine.UI;

namespace Photon.Chat.Demo
{
    [RequireComponent(typeof(ChatGui))]
    public class NamePickGui : MonoBehaviour
    {
        private const string _userNamePlayerPref = "NamePickUserName";
        [SerializeField]
        private InputField _idInput;

        public void Start()
        {

            string prefsName = PlayerPrefs.GetString(_userNamePlayerPref);
            if (!string.IsNullOrEmpty(prefsName))
            {
                _idInput.text = prefsName;
            }
        }


        // new UI will fire "EndEdit" event also when loosing focus. So check "enter" key and only then StartChat.
        public void EndEditOnEnter()
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
                OnClickButtonConnect();
        }

        public void OnClickButtonConnect()
        {
            ChatGui chatNewComponent = FindObjectOfType<ChatGui>();
            chatNewComponent.UserName = _idInput.text.Trim();
            chatNewComponent.Connect();
            enabled = false;

            PlayerPrefs.SetString(_userNamePlayerPref, chatNewComponent.UserName);
        }
    }
}