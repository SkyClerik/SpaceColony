using System;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class HeroIconElement : VisualElement
    {
        private Action<HeroIconElement> _callback;
        public ActorData actorData;

        public HeroIconElement(VisualElement rootElement, ActorData actorDataLink, Action<HeroIconElement> callback)
        {
            _callback = callback;
            actorData = actorDataLink;

            var heroIconTemplate = UserInterfaceShare.Instance.HeroIconTemplete.Instantiate();
            Add(heroIconTemplate);

            rootElement.Add(this);

            //RegisterCallback<MouseDownEvent>(OnMouseDown);
            RegisterCallback<MouseUpEvent>(OnMouseUp);
        }

        ~HeroIconElement()
        {
            //UnregisterCallback<MouseDownEvent>(OnMouseDown);
            UnregisterCallback<MouseUpEvent>(OnMouseUp);
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
    }
}