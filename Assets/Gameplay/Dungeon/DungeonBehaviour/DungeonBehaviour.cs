using PoolObjectSystem;
using UnityEngine;
using Gameplay.UI;
using Gameplay.Data;
using Behavior;

namespace Gameplay
{
    [RequireComponent(typeof(BoxCollider))]
    public class DungeonBehavior : MonoBehaviour
    {
        [SerializeField]
        private Transform _parking;
        [SerializeField]
        private GameObject _billboardObject;
        [SerializeField]
        private GameObject _transportPoolObjectID;
        [SerializeField]
        private ActorParty _actorParty = new ActorParty(partyLimit: 3);
        [SerializeField]
        private DungeonDefinition _dungeonDefinition;

        private CarBehavior _carInMission;
        private Billboard _billboard;

        public ActorParty GetActorParty => _actorParty;

        private void Awake()
        {
            PlayerDungeonContainer.Instance.AddDungeon(this);
        }

        public void SystemClicked()
        {
            ShowPage();
        }

        private void OnMouseDown()
        {
            if (UserInterfaceRaycaster.Instance.IsPickingMode)
                return;

            ShowPage();
        }

        private void ShowPage()
        {
            if (_dungeonDefinition.Equals(null))
                return;

            DungeonPage.Instance.Show(dungeonBehavior: this, dungeonDefinition: _dungeonDefinition);
        }

        public void SendOnMission(ActorParty actorParty)
        {
            _actorParty = actorParty;
            var car = Pool.Instance.Get(_transportPoolObjectID);
            if (car.TryGetComponent(out _carInMission))
            {
                PlayerBuildsContainer playerBuildsContainer = PlayerBuildsContainer.Instance;
                BuildInfo commandCenterInfo = playerBuildsContainer.GetCommandCenterInfo;
                var commandCenter = playerBuildsContainer.GetBuildingBehavior(commandCenterInfo);
                _carInMission.MoveToPoint(startingPosition: commandCenter.GetParking.position, destination: _parking, dungeonBehavior: this);

                DungeonEvents dungeonEvents = DungeonEvents.Instance;
                dungeonEvents.OnQuestStarting(dungeonBehavior: this);
                dungeonEvents.CarTaskComplete += OnCarTaskComplete;
            }
        }

        private void OnCarTaskComplete(CarBehavior carBehavior, Transform destination)
        {
            if (_carInMission.Equals(carBehavior))
            {
                DungeonEvents.Instance.CarTaskComplete -= OnCarTaskComplete;
                carBehavior.gameObject.SetActive(false);

                if (destination.Equals(_parking.transform))
                {
                    Debug.Log($"[Car] Машина достигла данж: {destination.name}", carBehavior.gameObject);
                    WorldBillboardsPage.Instance.BillboardShow(target: gameObject, _dungeonDefinition.Icon, timerTime: _dungeonDefinition.GetWaitingTime, OnBillboardTimeUp);
                }

                PlayerBuildsContainer playerBuildsContainer = PlayerBuildsContainer.Instance;
                BuildInfo commandCenterInfo = playerBuildsContainer.GetCommandCenterInfo;
                var commandCenter = playerBuildsContainer.GetBuildingBehavior(commandCenterInfo);
                if (destination.Equals(commandCenter.GetParking))
                {
                    Debug.Log($"[Car] Машина вернулась в: {destination.name}", carBehavior.gameObject);
                    MissionFinished();
                }
            }
        }

        private void OnBillboardTimeUp()
        {
            Debug.Log($"[Dungeon] Время вышло. {gameObject.name} готов отправить машину обратно", gameObject);
            var car = Pool.Instance.Get(_transportPoolObjectID);
            if (car.TryGetComponent(out _carInMission))
            {
                PlayerBuildsContainer playerBuildsContainer = PlayerBuildsContainer.Instance;
                BuildInfo commandCenterInfo = playerBuildsContainer.GetCommandCenterInfo;
                var commandCenter = playerBuildsContainer.GetBuildingBehavior(commandCenterInfo);
                _carInMission.MoveToPoint(startingPosition: _parking.position, destination: commandCenter.GetParking, dungeonBehavior: this);

                DungeonEvents dungeonEvents = DungeonEvents.Instance;
                dungeonEvents.CarTaskComplete += OnCarTaskComplete;
            }
        }

        public void MissionFinished()
        {
            int partyGS = _actorParty.GetPartyGS();
            CalculateMissionResult(partyGS);
        }

        private void CalculateMissionResult(int partyGS)
        {
            if (partyGS >= _dungeonDefinition.ExpFromWin)
            {
                //TODO Дополнить усталостью
                MissionSuccess();
            }
            else
            {
                //TODO Дополнить потерями
                MissionFail();
            }

            void MissionSuccess()
            {
                //TODO Вызвать панель уведомлений и показать результат
                PlayerReputation.AddReputation(_dungeonDefinition.AddReputation);
                _actorParty.AddPartyExperience(_dungeonDefinition);

                _dungeonDefinition.GiveOutLoot();
            }

            void MissionFail()
            {
                PlayerReputation.AddReputation(-_dungeonDefinition.RemoveReputation);
                _actorParty.AddPartyExperience(_dungeonDefinition, multiple: 0.3f);
            }
        }
    }
}