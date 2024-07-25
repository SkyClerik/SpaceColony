
using System.Collections.Generic;
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
        private const string _applyButtonName = "apply_button";

        private Button _hideButton;
        private const string _hideButtonName = "hide_button";

        private Label _titleElement;
        private const string _titleElementName = "title";

        private Label _descriptionElement;
        private const string _descriptionElementName = "description";

        private const string _iconName = "icon";

        private VisualElement _cells;
        private const string _cellsName = "cells";
        private List<HeroIconElement> _heroIconElements = new List<HeroIconElement>();

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

        private void RepaintCalls()
        {
            _cells = _rootElement.Q<VisualElement>(_cellsName);
            _cells.Clear();

            for (int i = 0; i < _questData.ActorTypes.Count; i++)
            {
                HeroIconElement heroIconElement = new HeroIconElement(_cells, null, OnMouseDownCallback);
                _heroIconElements.Add(heroIconElement);

                VisualElement icon = heroIconElement.Q(_iconName);
                icon.style.backgroundImage = null;
                icon.Q<VisualElement>("up_panel").style.display = DisplayStyle.None;
                icon.Q<VisualElement>("progress_bar").style.display = DisplayStyle.None;
            }
        }

        private void OnMouseDownCallback(HeroIconElement heroIconElement)
        {
            for (int i = 0; i < _heroIconElements.Count; i++)
            {
                if (heroIconElement == _heroIconElements[i])
                {
                    RemoveActor(heroIconElement);
                    Quest.CurrentQuestSelected?.RemoveActor(heroIconElement.actorData);
                    heroIconElement.actorData = null;
                    _heroIconElements[i] = heroIconElement;
                }
            }
        }

        public void AddActor(ActorData actorData)
        {
            for (int i = 0; i < _heroIconElements.Count; i++)
            {
                if (_heroIconElements[i].actorData == null)
                {
                    actorData.Busy = true;
                    _heroIconElements[i].actorData = actorData;

                    var icon = _heroIconElements[i].Q(_iconName);
                    icon.style.backgroundImage = new StyleBackground(actorData.Icon);
                    //icon.style.display = DisplayStyle.Flex;
                    HUDUserInterface.Instance.SetShadow(actorData);
                    return;
                }
            }

            Debug.Log($"Не удается добавить актера!");
        }

        public void RemoveActor(HeroIconElement heroIconElement)
        {
            if (heroIconElement.actorData == null)
                return;

            heroIconElement.actorData.Busy = false;

            VisualElement icon = heroIconElement.Q<VisualElement>(_iconName);
            icon.style.backgroundImage = null;

            HUDUserInterface.Instance.SetShadow(heroIconElement.actorData);
        }

        private void ApplyButton_Clicked()
        {
            List<ActorData> actors = new List<ActorData>();
            foreach (var item in _heroIconElements)
                actors.Add(item.actorData);
            
            _quest.SendOnMission(actors);
            Hide();
        }

        private void HideButton_Clicked() => Hide();

        public void View(Quest quest)
        {
            if (_userInterfaceShare.CurrentDocument != null)
                return;

            _quest = quest;
            _questData = quest.QuestData;

            RepaintCalls();

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
            Quest.CurrentQuestSelected = null;
        }
    }
}