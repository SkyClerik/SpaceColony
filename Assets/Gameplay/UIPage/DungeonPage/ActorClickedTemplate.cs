using System;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class ActorClickedTemplate : VisualElement
    {
        private byte _index;
        private const string _iconName = "icon";
        private VisualElement _icon;
        private const string _buttonName = "button";
        private Button _button;
        private Action<byte> _callback;

        public VisualElement Icon => _icon;

        public ActorClickedTemplate(byte index, VisualTreeAsset template, VisualElement root, Action<byte> callback)
        {
            _index = index;
            template.CloneTree(this);
            root.Add(this);
            _callback = callback;
            _icon = this.Q(_iconName);
            _button = this.Q<Button>(_buttonName);
            _button.clicked += Button_clicked;
        }

        private void Button_clicked()
        {
            _callback?.Invoke(_index);
        }
    }
}