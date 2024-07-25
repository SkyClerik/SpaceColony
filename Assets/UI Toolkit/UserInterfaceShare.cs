using UnityEngine.UIElements;
using UnityEngine;

namespace Gameplay
{
    public class UserInterfaceShare : Singleton<UserInterfaceShare>
    {
        [SerializeField]
        private VisualTreeAsset _heroIconTemplete;

        public VisualTreeAsset HeroIconTemplete=> _heroIconTemplete;
        public UIDocument CurrentDocument { get; set; }
    }
}