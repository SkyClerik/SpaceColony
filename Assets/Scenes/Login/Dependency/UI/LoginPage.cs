using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoginPage : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _rootElement;

    private TextField _loginTextField;
    private TextField _mailTextField;
    private TextField _passwordTextField;
    private const string _loginTextFieldName = "text_field_login";
    private const string _mailTextFieldName = "text_field_mail";
    private const string _passwordTextFieldName = "text_field_password";
    private Button _signinButton;
    private Button _registrationButton;
    private Button _backButton;
    private const string _signinButtonName = "signin_button";
    private const string _registrationButtonName = "registration_button";
    private const string _backButtonName = "back_button";

#if UNITY_EDITOR
    [SerializeField] private Object _mainMenuScene = null;
    [SerializeField] private Object _developQuestScene = null;
    bool IsMainSceneValid
    {
        get
        {
            if (_mainMenuScene == null)
                return false;
            return _mainMenuScene.GetType().Equals(typeof(SceneAsset));
        }
    }

    bool IsDevelopSceneValid
    {
        get
        {
            if (_developQuestScene == null)
                return false;
            return _developQuestScene.GetType().Equals(typeof(SceneAsset));
        }
    }

    private void OnValidate()
    {
        if (!IsMainSceneValid)
            _mainMenuScene = null;

        if (!IsDevelopSceneValid)
            _developQuestScene = null;
    }

#endif

    private void Awake() => Init();

    private void Init()
    {
        _document = GetComponent<UIDocument>();
        _rootElement = _document.rootVisualElement;

        _loginTextField = _rootElement.Q<TextField>(_loginTextFieldName);
        _mailTextField = _rootElement.Q<TextField>(_mailTextFieldName);
        _passwordTextField = _rootElement.Q<TextField>(_passwordTextFieldName);
        _signinButton = _rootElement.Q<Button>(_signinButtonName);
        _registrationButton = _rootElement.Q<Button>(_registrationButtonName);
        _backButton = _rootElement.Q<Button>(_backButtonName);

        _signinButton.clicked += SigninButtonClicked;
        _registrationButton.clicked += RegistrationButtonClicked;
        _backButton.clicked += BackButtonClicked;
    }

    private void OnDestroy()
    {
        _signinButton.clicked -= SigninButtonClicked;
        _registrationButton.clicked -= RegistrationButtonClicked;
        _backButton.clicked -= BackButtonClicked;
    }

    private void SigninButtonClicked()
    {
        SceneManager.LoadScene(_developQuestScene.name);
    }

    private void RegistrationButtonClicked()
    {
        SceneManager.LoadScene(_developQuestScene.name);
    }

    private void BackButtonClicked()
    {
        SceneManager.LoadScene(_mainMenuScene.name);
    }
}