using Gameplay.Data;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class ItemIconTemplate : VisualElement
    {
        private Action<ItemIconTemplate> _callback;
        private const string _iconName = "icon";
        public ItemDefinition ItemDefinition;

        public ItemIconTemplate(VisualElement rootElement, ItemDefinition itemDefinition, Action<ItemIconTemplate> callback)
        {
            _callback = callback;
            ItemDefinition = itemDefinition;

            var itemIconTemplate = UserInterfaceShare.Instance.GetItemIconTemplate.Instantiate();
            Add(itemIconTemplate);

            VisualElement icon = this.Q(_iconName);
            icon.style.backgroundImage = new StyleBackground(itemDefinition.Icon);

            rootElement.Add(this);

            //RegisterCallback<MouseDownEvent>(OnMouseDown);
            RegisterCallback<MouseUpEvent>(OnMouseUp);
        }

        ~ItemIconTemplate()
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

        public void SetShadow(ActorDefinition actorData)
        {
            if (actorData.Busy == true)
            {
                style.backgroundColor = Color.gray;
                return;
            }
            else
            {
                style.backgroundColor = UserInterfaceShare.Instance.TransparentColor;
            }
        }
    }
}