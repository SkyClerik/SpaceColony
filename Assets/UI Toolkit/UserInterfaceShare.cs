using UnityEngine.UIElements;
using UnityEngine;

namespace Gameplay
{
    public class UserInterfaceShare : Singleton<UserInterfaceShare>
    {
        [SerializeField]
        private VisualTreeAsset _heroIconTemplete;
        [SerializeField]
        private VisualTreeAsset _itemIconTemplete;

        [SerializeField]
        private VisualTreeAsset _globalResourceTemplate;

        public VisualTreeAsset HeroIconTemplete => _heroIconTemplete;
        public VisualTreeAsset ItemIconTemplete => _itemIconTemplete;
        public VisualTreeAsset GlobalResourceTemplate => _globalResourceTemplate;

        public UIDocument CurrentDocument { get; set; }

        public Color TransparentColor = new Color(1, 1, 1, 0);
    }
}