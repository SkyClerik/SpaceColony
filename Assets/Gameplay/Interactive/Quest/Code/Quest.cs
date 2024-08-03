using AvatarLogic;
using PoolObjectSystem;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
using UnityEngine.UIElements;

namespace Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class Quest : MonoBehaviour
    {
        public static Quest CurrentQuestSelected;

        [SerializeField]
        private Transform _parkingPosition;
        [SerializeField]
        private GameObject _billboardPoint;
        private Billboard _billboard;
        [SerializeField]
        private PoolObjectID _transportPoolObjectID;

        private bool _inProgress = false;
        private CarBehaviour _carInMission;
        private List<ActorData> _party = new List<ActorData>();
        private int _partyLimit;
        private QuestData _questData;

        public Transform ParkingPosition => _parkingPosition;
        public QuestData GetQuestData => _questData;
        private bool IsFullParty => _party.Count == _partyLimit ? true : false;

        public void AddQuest(QuestData questData)
        {
            if (_questData != null)
                return;

            _questData = questData;

            WorldBillboards worldBillboards = WorldBillboards.Instance;
            _billboard = worldBillboards.GetFreeBillboardFrom(_billboardPoint);
            worldBillboards.Relocation(_billboard);
            _billboard.Timeout = _questData.GetWaitingTime;
            _billboard.Tick(null);
            _billboard.style.display = DisplayStyle.Flex;
            QuestInit();
        }

        private void QuestInit()
        {
            _questData.ParkingPosition = _parkingPosition;
            _partyLimit = _questData.ActorTypes.Count;
            _party.Clear();
            InvokeRepeating(nameof(Tick), 1, 1);
        }

        private void Tick()
        {
            _billboard?.Tick(() =>
            {
                CancelInvoke(nameof(Tick));
                _billboard.Hide();
                RemoveQuest();
            });
        }

        private void OnMouseDown()
        {
            if (_inProgress || _questData == null)
                return;

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

            CancelInvoke(nameof(Tick));
            _billboard.Hide();
        }

        public void AddActor(ref ActorData actorData)
        {
            if (IsFullParty)
                return;

            if (actorData.Busy)
                return;

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
            if (_questData == null)
            {
                Debug.Log($"говорят тут null: {_questData}");
                Debug.Log($"Вопрос, почему тут исчезли данные");
            }

            int exp = 0;
            float multiplier = 1;
            foreach (ActorData actorData in _party)
            {
                foreach (var typeElement in _questData.ActorTypes)
                {
                    if (typeElement.ActorType == actorData.Type)
                    {
                        multiplier = typeElement.Multiplier;
                    }
                }

                multiplier = (multiplier < 1) ? 1 : multiplier;
                exp += Mathf.FloorToInt(actorData.Experience * multiplier);
            }

            CalculateMissionResult(exp);
            RemoveQuest();
        }

        private void CalculateMissionResult(int exp)
        {
            if (exp >= _questData.ExpFromWin)
            {
                //TODO Дополнить усталостью
                MissionSucces();
            }
            else
            {
                //TODO Дополнить потерями
                MissionFail();
            }
        }

        private void MissionSucces()
        {
            //TODO Вызвать панель уведомлений и покащать результат
            Guild.Instance.AddReputation(_questData.AddReputation);
            foreach (ActorData actorData in _party)
            {
                actorData.Experience += 2;
            }
        }

        private void MissionFail()
        {
            Guild.Instance.AddReputation(-_questData.RemoveReputation);
            foreach (ActorData actorData in _party)
            {
                actorData.Experience += 1;
            }
        }

        private void RemoveQuest()
        {
            _inProgress = false;
            _questData = null;
        }

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