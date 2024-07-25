using AvatarLogic;
using PoolObjectSystem;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
using System.Linq;

namespace Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class Quest : MonoBehaviour
    {
        public static Quest CurrentQuestSelected;

        [SerializeField]
        private QuestData _questData;
        [SerializeField]
        private Transform _parkingPosition;
        [SerializeField]
        private Billboard _billboard;
        [SerializeField]
        private PoolObjectID _transportPoolObjectID;

        private bool _inProgress = false;
        private CarBehaviour _carInMission;
        private List<ActorData> _party = new List<ActorData>();
        private int _partyLimit;

        public Transform ParkingPosition => _parkingPosition;
        public QuestData GetQuestData => _questData;
        private bool IsFullParty => _party.Count == _partyLimit ? true : false;

        private void Start()
        {
            if (_questData != null)
                QuestInit();
        }

        public void AddQuest(QuestData questData)
        {
            if (_questData != null)
                return;

            _questData = questData;
            QuestInit();
        }

        private void QuestInit()
        {
            _questData.ParkingPosition = _parkingPosition;
            _partyLimit = _questData.ActorTypes.Count;
            _party.Clear();
            _billboard?.gameObject.SetActive(true);
        }

        private void OnMouseDown()
        {
            if (_inProgress || _questData == null)
                return;

            QuestInit();

            CurrentQuestSelected = this;
            QuestUserInterface.Instance.ShowInterface();
        }

        public void SendOnMission(List<ActorData> actors)
        {
            var car = Pool.Instance.Get(_transportPoolObjectID);
            if (car.TryGetComponent(out CarBehaviour _carInMission))
            {
                _carInMission.MoveToQuest(quest: this, _parkingPosition, actors);
                _inProgress = true;
            }
        }

        public void AddActor(ref ActorData actorData)
        {
            Debug.Log($"IsFullParty {IsFullParty}");
            if (IsFullParty)
                return;

            Debug.Log($"actorData.Busy {actorData.Busy}");
            if (actorData.Busy)
                return;

            Debug.Log($"AddActor");
            _party.Add(actorData);
            actorData.Busy = true;
            QuestUserInterface.Instance.AddActor(actorData);
        }

        public void RemoveActor(ActorData actorData)
        {
            for (int i = 0; i < _party.Count; i++)
            {
                if (_party[i] == actorData)
                {
                    actorData.Busy = false;
                    _party.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveParty()
        {
            for (int i = 0; i < _party.Count; i++)
            {
                RemoveActor(_party[i]);
            }
        }

        public void MissionFinished()
        {
            _inProgress = false;

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