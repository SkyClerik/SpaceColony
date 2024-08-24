using UnityEngine.UIElements;
using UnityEngine;

namespace Gameplay
{
    public class UserInterfaceShare : Singleton<UserInterfaceShare>
    {
        [SerializeField]
        private VisualTreeAsset _heroIconTemplete;

        [SerializeField]
        private VisualTreeAsset _globalResourceTemplate;

        public VisualTreeAsset HeroIconTemplete => _heroIconTemplete;
        public VisualTreeAsset GlobalResourceTemplate => _globalResourceTemplate;

        public UIDocument CurrentDocument { get; set; }
    }
}