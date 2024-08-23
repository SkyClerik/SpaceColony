using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using QuestSystem;

namespace Gameplay.UI
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

        private Label _posTitleElement;
        private const string _posTitleElementName = "pos_title";

        private Label _descriptionElement;
        private const string _descriptionElementName = "description";

        private const string _iconName = "icon";

        private VisualElement _cells;
        private const string _cellsName = "cells";
        private List<HeroIconElement> _heroIconElements = new List<HeroIconElement>();

        private UserInterfaceShare _userInterfaceShare;

        private void Awake() => InitDocument();

        private void InitDocument()
        {
            _userInterfaceShare = UserInterfaceShare.Instance;

            _document = GetComponent<UIDocument>();
            _rootElement = _document.rootVisualElement;

            _applyButton = _rootElement.Q<Button>(_applyButtonName);
            _applyButton.clicked += ApplyButton_Clicked;

            _hideButton = _rootElement.Q<Button>(_hideButtonName);
            _hideButton.clicked += HideButton_Clicked;

            _titleElement = _rootElement.Q<Label>(_titleElementName);
            _posTitleElement = _rootElement.Q<Label>(_posTitleElementName);
            _descriptionElement = _rootElement.Q<Label>(_descriptionElementName);

            Hide();
        }

        private void ClickedEditorButton()
        {
            throw new System.NotImplementedException();
        }

        private void RepaintCalls()
        {
            _cells = _rootElement.Q<VisualElement>(_cellsName);
            _cells.Clear();
            _heroIconElements.Clear();

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
            if (heroIconElement.actorData == null)
                return;

            for (int i = 0; i < _heroIconElements.Count; i++)
            {
                if (heroIconElement == _heroIconElements[i])
                {
                    RemoveActor(heroIconElement);
                    Quest.CurrentQuestSelected?.RemoveActor(heroIconElement.actorData);
                    heroIconElement.actorData = null;
                    _heroIconElements[i] = heroIconElement;
                    ShiftingHeroIconElements(curElement: i);
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
                    //HUDUserInterface.Instance.SetShadow(actorData);
                    return;
                }
            }

            Debug.Log($"Не удается добавить актера!");
        }

        public void RemoveActor(HeroIconElement heroIconElement)
        {
            heroIconElement.actorData.Busy = false;

            VisualElement icon = heroIconElement.Q<VisualElement>(_iconName);
            icon.style.backgroundImage = null;

            //HUDUserInterface.Instance.SetShadow(heroIconElement.actorData);
        }

        private void RemoveParty()
        {
            for (int i = 0; i < _heroIconElements.Count; i++)
            {
                if (_heroIconElements[i].actorData == null)
                    continue;

                RemoveActor(_heroIconElements[i]);
            }
        }

        private void ShiftingHeroIconElements(int curElement)
        {
            for (int c = curElement, n = curElement + 1; c < _heroIconElements.Count && n < _heroIconElements.Count; c++, n++)
            {
                _heroIconElements[c].actorData = _heroIconElements[n].actorData;

                if (_heroIconElements[c].actorData != null)
                {
                    var curIcon = _heroIconElements[c].Q(_iconName);
                    curIcon.style.backgroundImage = new StyleBackground(_heroIconElements[c].actorData.Icon);
                }

                var nextIcon = _heroIconElements[n].Q<VisualElement>(_iconName);
                nextIcon.style.backgroundImage = null;
                _heroIconElements[n].actorData = null;
            }
        }

        private void ApplyButton_Clicked()
        {
            List<ActorData> actors = new List<ActorData>();
            foreach (var item in _heroIconElements)
            {
                if (item.actorData != null)
                    actors.Add(item.actorData);
            }

            if (actors.Count == 0)
                return;

            _quest.SendOnMission(actors);
            Hide();
        }

        private void HideButton_Clicked()
        {
            _quest.RemoveParty();
            RemoveParty();
            Hide();
        }

        public void ShowInterface()
        {
            _quest = Quest.CurrentQuestSelected;
            _questData = Quest.CurrentQuestSelected.GetQuestData;

            if (_quest == null || _questData == null)
                return;

            if (_userInterfaceShare.CurrentDocument != null)
                return;

            RepaintCalls();

            UpdateDisplayInfo();

            View();
        }

        private void UpdateDisplayInfo()
        {
            _titleElement.text = _questData.Title;
            _posTitleElement.text = _questData.PosTitle;
            _descriptionElement.text = _questData.Description;
        }

        private void View()
        {
            _rootElement.style.display = DisplayStyle.Flex;
            _userInterfaceShare.CurrentDocument = _document;
        }

        private void Hide()
        {
            _rootElement.style.display = DisplayStyle.None;
            _userInterfaceShare.CurrentDocument = null;
            Quest.CurrentQuestSelected = null;
        }
    }
}