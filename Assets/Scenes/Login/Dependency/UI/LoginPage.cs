using Firebase;
using Firebase.Auth;
using Firebase.Database;
using SkyClerikExt;
using System.Collections;
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
    private Label _mailExeption;
    private Label _passwordExeption;
    private const string _mailExeptionName = "mail_exeption";
    private const string _passwordExeptionName = "password_exeption";

    private bool _isRegistration = false;

    [SerializeField]
    private QuestSystem.QuestData _questData;

    private DatabaseReference _databaseReference;
    private DependencyStatus _dependencyStatus;
    private FirebaseAuth _firebaseAuth;
    private FirebaseUser _firebaseUser;

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

    private void Awake()
    {
        InitDocument();
    }

    private void Start()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void InitDocument()
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

        _mailExeption = _rootElement.Q<Label>(_mailExeptionName);
        _passwordExeption = _rootElement.Q<Label>(_passwordExeptionName);
    }

    private void OnDestroy()
    {
        _signinButton.clicked -= SigninButtonClicked;
        _registrationButton.clicked -= RegistrationButtonClicked;
        _backButton.clicked -= BackButtonClicked;
    }

    private void SigninButtonClicked()
    {
        //SaveData();
        //StartCoroutine(LoadData());
        StartCoroutine(SignIn());
        //SceneManager.LoadScene(_developQuestScene.name);
    }

    private void RegistrationButtonClicked()
    {
        if (_isRegistration)
        {
            if (UtilsExt.IsNotNullOrEmptyAny(_mailTextField.text, _passwordTextField.text, _loginTextField.text))
            {
                bool isMailValid = _mailTextField.text.IsValidEmail();
                if (isMailValid)
                {
                    SetLabel(_mailExeption, false, "Проверка успешно пройдена");
                    StartCoroutine(Register());
                }
                else
                {
                    SetLabel(_mailExeption, true, "Проверьте правильность указанной почты");
                }
            }
        }
        else
        {
            SetRegistrarion(true);
        }
    }

    private void BackButtonClicked()
    {
        if (_isRegistration)
            SetRegistrarion(false);
        else
            SceneManager.LoadScene(_mainMenuScene.name);
    }

    private void SetLabel(Label label, bool enable, string text)
    {
        label.text = text;
        label.style.display = enable ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void SetRegistrarion(bool enable)
    {
        if (enable)
        {
            _loginTextField.style.display = DisplayStyle.Flex;
            _isRegistration = true;
        }
        else
        {
            _loginTextField.style.display = DisplayStyle.None;
            _isRegistration = false;
            SetLabel(_passwordExeption, false, null);
            SetLabel(_passwordExeption, false, null);
        }
    }

    private void SaveData()
    {
        var data = JsonUtility.ToJson(_questData);
        _databaseReference.Child("Main").SetRawJsonValueAsync(data);
        Debug.Log("SaveData");
    }

    private IEnumerator LoadData()
    {
        var data = _databaseReference.Child("Main").GetValueAsync();
        yield return new WaitUntil(() => data.IsCompleted);

        if (data.Exception != null)
        {
            Debug.LogError($"Load Exception: {data.Exception}");
        }
        else if (data.Result == null)
        {
            Debug.Log($"Load Result: {data.Result}");
        }
        else
        {
            DataSnapshot snapshot = data.Result;
            string text = snapshot.Child("Main").GetRawJsonValue();
            QuestSystem.QuestData result = Instantiate(_questData);
            JsonUtility.FromJsonOverwrite(text, result);
            Debug.Log($"result: {result.Description}");
        }

        Debug.Log("LoadData");
    }

    public IEnumerator Register()
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_mailTextField.text, _passwordTextField.text);
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            print($"ошибка: {registerTask.Exception}");

            if (registerTask.Exception.ToString().Contains("The email address is already in use by another account"))
                SetLabel(_mailExeption, true, "Данная почта уже зарегистрирована");
            else
                SetLabel(_mailExeption, false, null);

            if (registerTask.Exception.ToString().Contains("The given password is invalid"))
                SetLabel(_passwordExeption, true, "Пароль должен содержать минимум 6 символов");
            else
                SetLabel(_passwordExeption, false, null);
        }
        else
        {
            SetLabel(_passwordExeption, false, "Регистрация завершина успешно");
            AuthResult result = registerTask.Result;
            _databaseReference.Child("Develops").Child(result.User.UserId).SetValueAsync(_loginTextField.text);
            SetRegistrarion(false);
        }
    }

    public IEnumerator SignIn()
    {
        var auth = FirebaseAuth.DefaultInstance;
        var loginTask = auth.SignInWithEmailAndPasswordAsync(_mailTextField.text, _passwordTextField.text);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            print("ответ сервера: " + loginTask.Exception);
        }
        else
        {
            print("ответ сервера: вход прошел успешно");
            Firebase.Auth.AuthResult result = loginTask.Result;
            var niks = _databaseReference.Child("usersName").Child(result.User.UserId).GetValueAsync();
            yield return new WaitUntil(predicate: () => niks.IsCompleted);

            if (niks.Exception != null)
            {
                print(niks.Exception);
            }
            else
            {
                DataSnapshot snapshot = niks.Result;
                print(snapshot.Value.ToString());
            }

            SceneManager.LoadScene(_developQuestScene.name);
        }
    }
}