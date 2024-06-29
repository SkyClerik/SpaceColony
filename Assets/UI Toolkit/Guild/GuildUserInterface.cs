using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay
{
    [RequireComponent(typeof(UIDocument))]
    public class GuildUserInterface : Singleton<GuildUserInterface>
    {
        private UIDocument _document;
        private VisualElement _rootElement;

        private Button _hideButton;
        private const string _hideButtonName = "HideButton";

        private UserInterfaceShare _userInterfaceShare;

        private void Awake() => Init();

        private void Init()
        {
            _userInterfaceShare = UserInterfaceShare.Instance;

            _document = GetComponent<UIDocument>();
            _rootElement = _document.rootVisualElement;

            _hideButton = _rootElement.Q<Button>(_hideButtonName);
            _hideButton.clicked += HideButton_Clicked;

            Hide();
        }

        private void HideButton_Clicked() => Hide();

        public void View()
        {
            if (_userInterfaceShare.CurrentDocument != null)
                return;

            _rootElement.style.display = DisplayStyle.Flex;
            _userInterfaceShare.CurrentDocument = _document;
        }

        public void Hide()
        {
            _rootElement.style.display = DisplayStyle.None;
            _userInterfaceShare.CurrentDocument = null;
        }
    }
}