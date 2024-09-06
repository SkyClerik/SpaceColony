using UnityEngine.UIElements;
using UnityEngine;
using System;
using OmlUtility;

namespace Gameplay.UI
{
    public class UserInterfaceShare : Singleton<UserInterfaceShare>, ITagObject
    {
        [SerializeField]
        private VisualTreeAsset _heroIconTemplate;
        [SerializeField]
        private VisualTreeAsset _itemIconTemplate;
        [SerializeField]
        private VisualTreeAsset _globalResourceTemplate;

        public VisualTreeAsset HeroIconTemplate => _heroIconTemplate;
        public VisualTreeAsset ItemIconTemplate => _itemIconTemplate;
        public VisualTreeAsset GlobalResourceTemplate => _globalResourceTemplate;

        public Color TransparentColor = new Color(1, 1, 1, 0);


        public Action<UIDocument> OpenNewPage;
        public void OnPageOpen(UIDocument uIDocument)
        {
            Debug.Log($"[Event] Пользователь открывает новую вкладку");
            OpenNewPage?.Invoke(uIDocument);
        }
    }
}