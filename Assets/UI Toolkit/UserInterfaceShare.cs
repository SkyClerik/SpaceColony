using UnityEngine.UIElements;
using UnityEngine;

namespace Gameplay
{
    public class UserInterfaceShare : Singleton<UserInterfaceShare>
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
    }
}