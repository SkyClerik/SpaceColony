using UnityEngine;
using UnityEngine.UIElements;
using Gameplay.Data;

namespace Gameplay.UI
{
    public class DungeonPage : UIPage<DungeonPage>
    {
        private Label _mainWindowTitle;
        private const string _mainWindowTitleName = "main_window_title";
        //private VisualElement _actorsArea;
        //private const string _actorsAreaName = "actors_area";

        private VisualElement _partyImage;
        private const string _partyImageName = "party_image";
        private Button _buttonGo;
        private const string _buttonGoName = "button_go";
        private Button _closeButton;
        private const string _closeButtonName = "close_button";

        private VisualTreeAsset _actorClickedTemplate;
        private ActorClickedTemplate[] _actorClickedTemplates = new ActorClickedTemplate[3];
        private ActorParty _party = new ActorParty();
        private DungeonDefinition _dungeonDefinition;
        private DungeonBehavior _dungeonBehavior;

        protected override void Awake()
        {
            base.Awake();
            InitFields();
        }

        private void OnEnable()
        {
            UserInterfaceShare.Instance.OpenNewPage += IsOpenNewPage;
        }

        private void OnDisable()
        {
            UserInterfaceShare.Instance.OpenNewPage -= IsOpenNewPage;
        }

        private void IsOpenNewPage(UIDocument uIDocument)
        {
            if (uIDocument != document)
                Hide();
        }

        private void InitFields()
        {
            _mainWindowTitle = rootElement.Q<Label>(_mainWindowTitleName);

            //_actorsArea = rootElement.Q(_actorsAreaName);
            _partyImage = rootElement.Q(_partyImageName);
            _partyImage.Clear();

            _actorClickedTemplate = UserInterfaceShare.Instance.GetActorClickedTemplate;
            for (byte i = 0; i < _actorClickedTemplates.Length; i++)
                _actorClickedTemplates[i] = new ActorClickedTemplate(i, _actorClickedTemplate, _partyImage, ClickedTemplateButton);

            _buttonGo = rootElement.Q<Button>(_buttonGoName);
            _buttonGo.clicked += ClickedButtonGo;

            _closeButton = rootElement.Q<Button>(_closeButtonName);
            _closeButton.clicked += ClickedCloseButton;
        }

        private void ClickedTemplateButton(byte index)
        {
            ActorSelected.Instance.Show(index, callbackActorData: ActorSelectedCallback, OnCallbackClose);
        }

        private void ActorSelectedCallback(byte index, ActorDefinition actorData)
        {
            var currentActor = _party.GetActorByIndex(index);
            if (currentActor != null)
                currentActor.Busy = false;

            _party.AddActor(ref actorData, index: index);
            //_actorClickedTemplates[index].Icon.style.backgroundImage = new StyleBackground(actorData.Icon);
            Debug.Log($"Я запускаю снова окно данжа");

            Show();
        }

        private void OnCallbackClose()
        {
            Show();
        }

        public void Show(DungeonBehavior dungeonBehavior, DungeonDefinition dungeonDefinition)
        {
            _dungeonBehavior = dungeonBehavior;
            _dungeonDefinition = dungeonDefinition;

            FillingOutTheFields();
            base.Show();
            Repaint();
        }

        private void FillingOutTheFields()
        {
            _mainWindowTitle.text = _dungeonDefinition.Title;
        }

        private void ClickedCloseButton()
        {
            _party.RemoveAllActors();
            Hide();
        }

        private void ClickedButtonGo()
        {
            if (_party.IsPartyEmpty)
            {
                Debug.Log($"В партии нет юнитов. Отправка запрещена");
            }
            else if (_dungeonBehavior.Progress != 0)
            {
                Debug.Log($"Данж уже фармится");
            }
            else
            {
                Debug.Log($"Отправка партии на задание");
                _dungeonBehavior.SendOnMission(_party);
                Hide();
            }
        }

        private void Repaint()
        {
            Debug.Log($"Repaint");
            //for (byte i = 0; i < _actorClickedTemplates.Length; i++)
            //{
            //    _actorClickedTemplates[i].Icon.style.backgroundImage = null;
            //}
        }
    }
}