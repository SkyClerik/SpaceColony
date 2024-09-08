using UnityEngine.UIElements;
using UnityEngine;
using System;
using OmlUtility;

namespace Gameplay.UI
{
    public class UserInterfaceShare : Singleton<UserInterfaceShare>, ITagObject
    {
        [SerializeField]
        private VisualTreeAsset _actorIconTemplate;
        [SerializeField]
        private VisualTreeAsset _actorClickedTemplate;
        [SerializeField]
        private VisualTreeAsset _itemIconTemplate;
        [SerializeField]
        private VisualTreeAsset _globalResourceTemplate;
        [SerializeField]
        private VisualTreeAsset _buildDrawingTemplate;

        public VisualTreeAsset GetActorIconTemplate => _actorIconTemplate;
        public VisualTreeAsset GetActorClickedTemplate => _actorClickedTemplate;
        public VisualTreeAsset GetItemIconTemplate => _itemIconTemplate;
        public VisualTreeAsset GetGlobalResourceTemplate => _globalResourceTemplate;
        public VisualTreeAsset GetBuildDrawingTemplate => _buildDrawingTemplate;

        public Color TransparentColor = new Color(1, 1, 1, 0);


        public Action<UIDocument> OpenNewPage;
        public void OnPageOpen(UIDocument uIDocument)
        {
            Debug.Log($"[Event] Пользователь открывает новую вкладку");
            OpenNewPage?.Invoke(uIDocument);
        }
    }
}