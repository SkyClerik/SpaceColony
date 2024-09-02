using Gameplay.Data;
using Gameplay.UI;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class ItemSelected : UIPage<ItemSelected>
    {
        private VisualElement _cells;
        private const string _cellsName = "cells";
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

            List<ItemDefinition> items = PlayerItemsContainer.Instance.Items;
            for (int i = 0; i < items.Count; i++)
            {
                ItemIconTemplate itemIconTemplate = new ItemIconTemplate(_cells, items[i], OnMouseDownCallback);
            }
        }

        private void OnMouseDownCallback(ItemIconTemplate itemDefinition)
        {
            if (_callbackItemDefinition != null)
            {
                _callbackItemDefinition.Invoke(_slotIndex, itemDefinition);
                Hide();
            }
        }

        byte _slotIndex;

        Action<byte, ItemIconTemplate> _callbackItemDefinition;
        public void Show(byte slotIndex, Action<byte, ItemIconTemplate> callback)
        {
            base.Show();
            RepaintCalls();
            _slotIndex = slotIndex;
            _callbackItemDefinition = callback;
        }

        public override void Show()
        {
            base.Show();
            RepaintCalls();
        }
    }
}
