using System;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class HeroIconElement : VisualElement
    {
        private TemplateContainer _heroIconTemplate;
        private Action<HeroIconElement> _callback;
        public ActorData actorData;

        public HeroIconElement(VisualElement rootElement, ActorData actorDataLink, Action<HeroIconElement> callback)
        {
            _callback = callback;
            actorData = actorDataLink;
            _heroIconTemplate = UserInterfaceShare.Instance.HeroIconTemplete.Instantiate();

            Add(_heroIconTemplate);
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
        //BUG: Нужно сохранять позицию курсора постоянно. Что бы нажатие не срабатывало после перемещения.
        //HELP: А нужно ли? В win это крутится колесиком а в андроиде нужно.Точно нужно. Но там события другие вроде как.
    }
}