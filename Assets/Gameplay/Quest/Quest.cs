using AvatarLogic;
using UnityEngine;

namespace Gameplay
{
    public class Quest : MonoBehaviour
    {
        [SerializeField]
        private QuestData _questData;
        [SerializeField]
        private Transform _parkingPosition;

        private bool _inProgress = false;
        private CarBehaviour _carInMission;

        public QuestData QuestData => _questData;

        public Transform ParkingPosition => _parkingPosition;

        public void SetProgress(bool progress) => _inProgress = progress;

        private void Awake()
        {
            _questData.ParkingPosition = ParkingPosition;
        }

        private void OnMouseDown()
        {
            if (_inProgress)
                return;

            QuestUserInterface.Instance.View(quest: this);
        }

        public void SendOnMission()
        {
            if (CarController.Instance.TryFindFreeCar(out _carInMission))
            {
                _carInMission.MoveToQuest(this, ParkingPosition);
                _inProgress = true;
            }
        }
    }
}