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
        private VisualElement _iconRootA;
        private VisualElement _slotAIcon;
        private const string _slotNameB = "slot_b";
        private VisualElement _slotBIcon;
        private VisualElement _iconRootB;
        private const string _slotNameC = "slot_c";
        private VisualElement _slotÑIcon;
        private VisualElement _iconRootC;

        private const string _iconRootName = "icon_root";
        private const string _buttonName = "button";
        private const string _iconName = "icon";

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            var heroSlots = rootElement.Q(_heroSlotsName);
            var slotA = heroSlots.Q(_slotNameA);
            var slotB = heroSlots.Q(_slotNameB);
            var slotC = heroSlots.Q(_slotNameC);

            _iconRootA = slotA.Q(_iconRootName);
            _slotAIcon = _iconRootA.Q(_iconName);
            var buttonA = slotA.Q<Button>(_buttonName);
            buttonA.clicked += ClickedSlotA;

            _iconRootB = slotB.Q(_iconRootName);
            _slotBIcon = _iconRootB.Q(_iconName);
            var buttonB = slotB.Q<Button>(_buttonName);
            buttonB.clicked += ClickedSlotB;

            _iconRootC = slotC.Q(_iconRootName);
            _slotÑIcon = _iconRootC.Q(_iconName);
            var buttonC = slotC.Q<Button>(_buttonName);
            buttonC.clicked += ClickedSlotC;
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
            //TODO: Ñòàðîãî ãåðîÿ íóæíî îòïóñòèòü åñëè îí áûë.

            switch (slotIndex)
            {
                case 0:
                    _slotAIcon.style.backgroundImage = new StyleBackground(actorData.Icon);
                    break;
                case 1:
                    _slotBIcon.style.backgroundImage = new StyleBackground(actorData.Icon);
                    break;
                case 2:
                    _slotÑIcon.style.backgroundImage = new StyleBackground(actorData.Icon);
                    break;
                default:
                    break;
            }
        }
    }
}