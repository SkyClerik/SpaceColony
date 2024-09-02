using System;
using System.Collections.Generic;
using Gameplay.Data;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class ActorSelected : UIPage<ActorSelected>
    {
        private VisualElement _cells;
        private const string _cellsName = "cells";
        private List<ActorIconTemplate> _callsList = new List<ActorIconTemplate>();
        private VisualElement _bottomPanel;
        private const string _bottomPanelName = "bottom_panel";
        private Button _buttonClose;
        private const string buttonCloseName = "button_close";

        protected override void Awake()
        {
            base.Awake();

            _bottomPanel = rootElement.Q(_bottomPanelName);
            _buttonClose = _bottomPanel.Q<Button>(buttonCloseName);
            _buttonClose.clicked += ClickedButtonClose;
        }

        private void ClickedButtonClose()
        {
            Hide();
        }

        public void RepaintCalls()
        {
            _cells = rootElement.Q<VisualElement>(_cellsName);
            _cells.Clear();

            List<ActorDefinition> actorData = PlayerActorsContainer.Instance.GetActorsData;
            for (int i = 0; i < actorData.Count; i++)
            {
                ActorIconTemplate heroIconElement = new ActorIconTemplate(_cells, actorData[i], OnMouseDownCallback);
                _callsList.Add(heroIconElement);
            }
        }

        private void OnMouseDownCallback(ActorIconTemplate heroIconElement)
        {
            if (heroIconElement.actorData.Busy)
                return;

            if (_callbackActorData != null)
            {
                _callbackActorData.Invoke(_slotIndex, heroIconElement.actorData);
                Hide();
            }
        }

        byte _slotIndex;
        Action<byte, ActorDefinition> _callbackActorData;
        public void Show(byte slotIndex, Action<byte, ActorDefinition> callbackActorData)
        {
            base.Show();
            RepaintCalls();
            _slotIndex = slotIndex;
            _callbackActorData = callbackActorData;
        }

        public override void Show()
        {
            base.Show();
            RepaintCalls();
        }
    }
}