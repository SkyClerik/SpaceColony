using UnityEngine.UIElements;
using SkyClerikExt;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Gameplay
{
    [RequireComponent(typeof(UIDocument))]
    public class HUDUserInterface : Singleton<HUDUserInterface>
    {
        private UIDocument _document;
        private VisualElement _rootElement;

        private Label _reputationValueElement;
        private const string _reputationValueElementName = "ReputationValueLabel";

        private const string _iconName = "icon";
        private const string _rightName = "right";

        private VisualElement _cells;
        private const string _cellsName = "cells";
        private List<HeroIconElement> _callsList = new List<HeroIconElement>();
        private Color _transparentColor = new Color(1, 1, 1, 0);

        private void OnEnable()
        {
            Guild.ReputationÑhange += UpdateReputation;
        }

        private void OnDestroy()
        {
            Guild.ReputationÑhange -= UpdateReputation;
        }

        private void Awake() => Init();

        private void Init()
        {
            _document = GetComponent<UIDocument>();
            _rootElement = _document.rootVisualElement;

            var right = _rootElement.Q(_rightName);
            _reputationValueElement = right.Q<Label>(_reputationValueElementName);
            LoadReputation();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void RepaintCalls(List<ActorData> actorDatas)
        {
            _cells = _rootElement.Q<VisualElement>(_cellsName);
            _cells.Clear();

            for (int i = 0; i < actorDatas.Count; i++)
            {
                HeroIconElement heroIconElement = new HeroIconElement(_cells, actorDatas[i], OnMouseDownCallback);
                _callsList.Add(heroIconElement);

                VisualElement icon = heroIconElement.Q(_iconName);
                icon.style.backgroundImage = new StyleBackground(actorDatas[i].Icon);
            }
        }

        private void OnMouseDownCallback(HeroIconElement heroIconElement)
        {
            Debug.Log($"HUDUserInterface OnMouseDownCallback ");
            if (heroIconElement.actorData.Busy)
                return;

            Quest.CurrentQuestSelected?.AddActor(ref heroIconElement.actorData);
        }

        public void SetShadow(ActorData actorData)
        {
            foreach (var heroIconElement in _callsList)
            {
                if (heroIconElement.actorData == actorData)
                {
                    if (actorData.Busy == true)
                    {
                        heroIconElement.style.backgroundColor = Color.gray;
                        return;
                    }
                    else
                    {
                        heroIconElement.style.backgroundColor = _transparentColor;
                    }
                }
            }
        }

        public void UpdateReputation(int value)
        {
            _reputationValueElement.text = value.ToString().ToPriceStyle();
        }

        public void LoadReputation()
        {
            UpdateReputation(Guild.Instance.GetReputation);
        }
    }
}