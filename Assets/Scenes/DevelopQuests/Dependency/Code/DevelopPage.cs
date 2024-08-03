using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DevelopPage : Singleton<DevelopPage>
{
    private UIDocument _document;
    private VisualElement _rootElement;

    private Button _sendButton;
    private const string _sendButtonName = "send_button";
    private Button _backButton;
    private const string _backButtonName = "back_button";

    [SerializeField] private Object _mainMenuScene = null;

#if UNITY_EDITOR
    bool IsValidSceneAsset
    {
        get
        {
            if (_mainMenuScene == null)
                return false;
            return _mainMenuScene.GetType().Equals(typeof(UnityEditor.SceneAsset));
        }
    }

    private void OnValidate()
    {
        if (!IsValidSceneAsset)
            _mainMenuScene = null;
    }
#endif

    private void Awake() => Init();

    private void Init()
    {
        _document = GetComponent<UIDocument>();
        _rootElement = _document.rootVisualElement;

        _sendButton = _rootElement.Q<Button>(_sendButtonName);
        _backButton = _rootElement.Q<Button>(_backButtonName);

        _sendButton.clicked += SignInButton_Clicked;
        _backButton.clicked += BackButton_Clicked;
    }

    private void SignInButton_Clicked()
    {

    }

    private void BackButton_Clicked()
    {
        SceneManager.LoadScene(_mainMenuScene.name);
    }
}
