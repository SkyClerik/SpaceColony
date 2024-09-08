using System;
using UnityEngine.UIElements;
using Gameplay.Data;

namespace Gameplay.UI
{
    public class ActorIconTemplate : VisualElement
    {
        private Action<ActorIconTemplate> _callback;
        private const string _iconName = "icon";
        public ActorDefinition actorData;

        public ActorIconTemplate(VisualElement rootElement, ActorDefinition actorDataLink, Action<ActorIconTemplate> callback)
        {
            _callback = callback;
            actorData = actorDataLink;

            var heroIconTemplate = UserInterfaceShare.Instance.GetActorIconTemplate.Instantiate();
            Add(heroIconTemplate);

            VisualElement icon = this.Q(_iconName);
            icon.style.backgroundImage = new StyleBackground(actorDataLink.Icon);

            rootElement.Add(this);

            //RegisterCallback<MouseDownEvent>(OnMouseDown);
            RegisterCallback<MouseUpEvent>(OnMouseUp);
        }

        //private void OnMouseDown(MouseDownEvent mouseEvent)
        //{
        //    if (mouseEvent.button == 0)
        //    {
        //        _callback?.Invoke(this);
        //    }
        //}

        private void OnMouseUp(MouseUpEvent mouseEvent)
        {
            if (mouseEvent.button == 0)
            {
                _callback?.Invoke(this);
            }
        }
        //BUG: Отследи нажатие на карточку и держи в поле VisualElement.
        //Когда поднимаем кнопку проверим тот же ли это VisualElement

        ~ActorIconTemplate()
        {
            //UnregisterCallback<MouseDownEvent>(OnMouseDown);
            UnregisterCallback<MouseUpEvent>(OnMouseUp);
            _callback = null;
        }
    }
}