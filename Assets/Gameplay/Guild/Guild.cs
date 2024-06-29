using UnityEngine;

namespace Gameplay
{
    public class Guild : Singleton<Guild>
    {
        [SerializeField]
        private Transform _parkingPosition;


        public Transform PparkingPosition => _parkingPosition;

        private void OnMouseDown()
        {
            GuildUserInterface.Instance.View();
        }
    }
}