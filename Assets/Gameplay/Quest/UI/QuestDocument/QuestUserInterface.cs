using AvatarLogic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class QuestUserInterface : Singleton<QuestUserInterface>
    {
        private UIDocument _document;
        private VisualElement _rootElement;

        private QuestData _questData;
        private Quest _quest;

        private Button _applyButton;
        private const string _applyButtonName = "ApplyButton";

        private Button _hideButton;
        private const string _hideButtonName = "HideButton";

        private Label _titleElement;
        private const string _titleElementName = "Title";

        private Label _descriptionElement;
        private const string _descriptionElementName = "Description";

        private UserInterfaceShare _userInterfaceShare;

        private void Awake() => Init();

        private void Init()
        {
            _userInterfaceShare = UserInterfaceShare.Instance;

            _document = GetComponent<UIDocument>();
            _rootElement = _document.rootVisualElement;

            _applyButton = _rootElement.Q<Button>(_applyButtonName);
            _applyButton.clicked += ApplyButton_Clicked;

            _hideButton = _rootElement.Q<Button>(_hideButtonName);
            _hideButton.clicked += HideButton_Clicked;

            _titleElement = _rootElement.Q<Label>(_titleElementName);
            _descriptionElement = _rootElement.Q<Label>(_descriptionElementName);

            Hide();
        }

        private void ApplyButton_Clicked()
        {
            _quest.SendOnMission();
            Hide();
        }

        private void HideButton_Clicked() => Hide();

        public void View(Quest quest)
        {
            if (_userInterfaceShare.CurrentDocument != null)
                return;

            _quest = quest;
            _questData = quest.QuestData;

            UpdateDisplayInfo();

            _rootElement.style.display = DisplayStyle.Flex;
            _userInterfaceShare.CurrentDocument = _document;
        }

        private void UpdateDisplayInfo()
        {
            _titleElement.text = _questData.Title;
            _descriptionElement.text = _questData.Description;
        }

        public void Hide()
        {
            _rootElement.style.display = DisplayStyle.None;
            _userInterfaceShare.CurrentDocument = null;
        }
    }
}