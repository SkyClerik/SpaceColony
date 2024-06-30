using AvatarLogic;
using PoolObjectSystem;
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
            var car = Pool.Instance.Get(PoolObjectID.Car);
            if (car.TryGetComponent(out CarBehaviour _carInMission))
            {
                _carInMission.MoveToQuest(this, ParkingPosition);
                SetProgress(true);
            }
        }

        public void MissionFinished()
        {
            SetProgress(false);

            MissionSucces();
            //MissionFail();

            RemoveQuest();
        }

        private void MissionSucces() => Guild.Instance.AddReputation(_questData.AddReputation);

        private void MissionFail() => Guild.Instance.AddReputation(-_questData.RemoveReputation);

        private void RemoveQuest() => _questData = null;
    }
}