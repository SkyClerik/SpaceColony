using AvatarLogic;
using PoolObjectSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class Quest : MonoBehaviour
    {
        [SerializeField]
        private QuestData _questData;
        [SerializeField]
        private Transform _parkingPosition;
        [SerializeField]
        private PoolObjectID _transportPoolObjectID;

        private bool _inProgress = false;
        private CarBehaviour _carInMission;
        private List<ActorData> _party = new List<ActorData>();
        private int _partyLimit;
        private int _curPartyCount;

        public static Quest CurrentQuestSelected;
        public QuestData QuestData { get => _questData; set => _questData = value; }
        public Transform ParkingPosition => _parkingPosition;
        private bool IsFullParty => _curPartyCount == _partyLimit ? true : false;


        public void SetProgress(bool progress) => _inProgress = progress;

        public void AddQuest(QuestData questData)
        {
            _questData = questData;
            _questData.ParkingPosition = _parkingPosition;
            _partyLimit = _questData.ActorTypes.Count;
            _party = new List<ActorData>(_partyLimit);
        }

        private void OnMouseDown()
        {
            if (_inProgress || _questData == null)
                return;

            CurrentQuestSelected = this;
            QuestUserInterface.Instance.View(quest: CurrentQuestSelected);
        }

        public void SendOnMission(List<ActorData> actors)
        {
            var car = Pool.Instance.Get(_transportPoolObjectID);
            if (car.TryGetComponent(out CarBehaviour _carInMission))
            {
                _carInMission.MoveToQuest(quest: this, _parkingPosition, actors);
                SetProgress(true);
            }
        }

        public void AddActor(ref ActorData actorData)
        {
            if (IsFullParty)
                return;

            if (actorData.Busy)
                return;

            _curPartyCount++;
            _party.Add(actorData);
            QuestUserInterface.Instance.AddActor(actorData);
            actorData.Busy = true;
        }

        public void RemoveActor(ActorData actorData)
        {
            for (int i = 0; i < _party.Count; i++)
            {
                if (_party[i] == actorData)
                {
                    actorData.Busy = false;
                    _party.RemoveAt(i);
                    _curPartyCount--;
                    break;
                }
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

        private void CalculateResult(out MissionResult missionResult)
        {
            missionResult = MissionResult.Fail;
        }

        private enum MissionResult : byte
        {
            Success,
            Fail,
        }
    }
}