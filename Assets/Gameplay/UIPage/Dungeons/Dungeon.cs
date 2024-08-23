using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class Dungeon : UIPage<Dungeon>
    {
        [SerializeField]
        private GlobalVolumeManager volumeManager;

        public GlobalVolumeManager GetVolumeManager => volumeManager;

        private const string _heroSlotsName = "hero_slots";
        private const string _slotNameA = "slot_a";
        private const string _slotNameB = "slot_b";
        private const string _slotNameC = "slot_c";

        private const string _iconRootName = "icon_root";
        private const string _buttonName = "button";

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            var heroSlots = rootElement.Q(_heroSlotsName);
            var slotNameA = heroSlots.Q(_slotNameA);
            var slotNameB = heroSlots.Q(_slotNameB);
            var slotNameC = heroSlots.Q(_slotNameC);

            var iconRootNameA = slotNameA.Q(_iconRootName);
            var buttonNameA = slotNameA.Q<Button>(_buttonName);
            buttonNameA.clicked += ClickedSlotA;

            var iconRootNameB = slotNameB.Q(_iconRootName);
            var buttonNameB = slotNameB.Q<Button>(_buttonName);
            buttonNameB.clicked += ClickedSlotB;

            var iconRootNameC = slotNameC.Q(_iconRootName);
            var buttonNameC = slotNameC.Q<Button>(_buttonName);
            buttonNameC.clicked += ClickedSlotC;
        }

        private void ClickedSlotA()
        {
            ActorSelected.Instance.Show(slotIndex: 0, callbackActorData: ActorSelectedCallback);
        }

        private void ClickedSlotB()
        {
            ActorSelected.Instance.Show(slotIndex: 1, callbackActorData: ActorSelectedCallback);
        }

        private void ClickedSlotC()
        {
            ActorSelected.Instance.Show(slotIndex: 2, callbackActorData: ActorSelectedCallback);
        }

        private void ActorSelectedCallback(byte slotIndex, ActorData actorData)
        {
            Debug.Log($"slotIndex: {slotIndex}  actorData: {actorData.name} ");
        }
    }
}