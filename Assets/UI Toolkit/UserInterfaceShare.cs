using UnityEngine.UIElements;

namespace Gameplay
{
    public class UserInterfaceShare : Singleton<UserInterfaceShare>
    {
        public UIDocument CurrentDocument { get; set; }
    }
}