using System;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class ActorClickedTemplate : VisualElement
    {
        private byte _index;
        private Button _button;
        private const string _buttonName = "button";
        private Action<byte> _callback;

        public ActorClickedTemplate(byte index, VisualTreeAsset template, VisualElement root, Action<byte> callback)
        {
            _index = index;
            template.CloneTree(this);
            root.Add(this);
            _callback = callback;
            _button = this.Q<Button>(_buttonName);
            _button.clicked += Button_clicked;
        }

        private void Button_clicked()
        {
            _callback?.Invoke(_index);
        }

        ~ActorClickedTemplate()
        {
            _callback = null;
        }
    }
}