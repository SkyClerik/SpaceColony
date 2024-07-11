using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UserIDForm : MonoBehaviour
{
    [SerializeField]
    public ChatPanelForm _chatpanelForm;
    [SerializeField]
    private GameObject _loadingPage;

    private UIDocument _document;
    private VisualElement _root;

    private Button _connectButton;
    private const string _connectButtonName = "connect_button";

    private TextField _inputText;
    private const string _inputTextName = "input_text";

    private const string _saveUserID = "Enter User ID";

    private void Start()
    {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;

        _connectButton = _root.Q<Button>(_connectButtonName);
        _connectButton.clicked += ConnectButtonClicked;

        _inputText = _root.Q<TextField>(_inputTextName);

        string prefsName = PlayerPrefs.GetString(_saveUserID);
        if (!string.IsNullOrEmpty(prefsName))
            _inputText.value = prefsName;
    }

    private void ConnectButtonClicked()
    {
        _chatpanelForm.UserID = _inputText.text.Trim();
        _chatpanelForm.OnButtonConnect();

        _loadingPage.SetActive(true);
        Hide();

        PlayerPrefs.SetString(_saveUserID, _chatpanelForm.UserID);
    }

    private void Hide()
    {
        _root.style.display = DisplayStyle.None;
    }
}