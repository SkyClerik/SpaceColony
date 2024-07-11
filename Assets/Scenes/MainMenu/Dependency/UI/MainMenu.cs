using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _rootElement;

    private Button _developButton;
    private const string _developButtonName = "develop_button";
    private Button _exitGameButton;
    private const string _exitGameButtonName = "exit_button";

#if UNITY_EDITOR
    [SerializeField] private Object _loginScene = null;
    bool IsValidSceneAsset
    {
        get
        {
            if (_loginScene == null)
                return false;
            return _loginScene.GetType().Equals(typeof(SceneAsset));
        }
    }

    private void OnValidate()
    {
        if (!IsValidSceneAsset)
            _loginScene = null;
    }

#endif

    private void Awake() => Init();

    private void Init()
    {
        _document = GetComponent<UIDocument>();
        _rootElement = _document.rootVisualElement;

        _developButton = _rootElement.Q<Button>(_developButtonName);
        _exitGameButton = _rootElement.Q<Button>(_exitGameButtonName);

        _developButton.clicked += DevelopButton_Clicked;
        _exitGameButton.clicked += ExitGameButton_Clicked;
    }

    private void DevelopButton_Clicked()
    {
        SceneManager.LoadScene(_loginScene.name);
    }

    private void ExitGameButton_Clicked() => Application.Quit();
}
