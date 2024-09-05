using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using SkyClericExt;

namespace Gameplay.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class HUDUserInterface : UIPage<HUDUserInterface>
    {
        private Label _reputationValueElement;
        private const string _reputationValueElementName = "ReputationValueLabel";

        private VisualElement _globalResourcesRoot;
        private const string _globalResourcesRootName = "global_resources_root";
        private List<ResourceDefinitionTemplate> globalResourceElements = new List<ResourceDefinitionTemplate>();

        private const string _line01 = "line_01";

        private const string _line02Name = "line_02";
        private const string _line02LeftName = "line_02_left";
        private const string _line02RightName = "line_02_right";

        private const string _line03Name = "line_03";
        private const string _line03LeftName = "line_03_left";
        private const string _line03RightName = "line_03_right";

        private Button _buttonCommandCenter;
        private const string _buttonCommandCenterName = "button_command_center";

        private Button _buttonActorSelected;
        private const string _buttonActorSelectedName = "button_actor_selected";

        private Button _buttonItems;
        private const string _buttonItemsName = "button_items";

        private Button _buttonDungeon;
        private const string _buttonDungeonName = "button_dungeon";

        private void OnEnable()
        {
            PlayerReputation.ReputationChange += UpdateReputation;
        }

        private void OnDestroy()
        {
            PlayerReputation.ReputationChange -= UpdateReputation;
        }

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            var line02 = rootElement.Q(_line02Name);
            var line02Right = line02.Q(_line02RightName);
            _reputationValueElement = line02Right.Q<Label>(_reputationValueElementName);
            LoadReputation();

            var line03 = rootElement.Q(_line03Name);
            var line03Left = line03.Q(_line03LeftName);
            var line03Right = line03.Q(_line03RightName);

            _buttonCommandCenter = line03Right.Q<Button>(_buttonCommandCenterName);
            _buttonCommandCenter.clicked += ClickedCommandCenter;

            _buttonActorSelected = line03Right.Q<Button>(_buttonActorSelectedName);
            _buttonActorSelected.clicked += ClickedActorSelected;

            _buttonItems = line03Right.Q<Button>(_buttonItemsName);
            _buttonItems.clicked += ClickedButtonItems;

            _buttonDungeon = line03Right.Q<Button>(_buttonDungeonName);
            _buttonDungeon.clicked += ClickedDungeon;

            _globalResourcesRoot = line03Left.Q(_globalResourcesRootName);
            _globalResourcesRoot.Clear();
            globalResourceElements = new List<ResourceDefinitionTemplate>();
            foreach (var resource in PlayerGlobalResourcesContainer.Instance.GetGlobalResources)
            {
                var newResource = new ResourceDefinitionTemplate(_globalResourcesRoot, resource);
                globalResourceElements.Add(newResource);
            }
        }

        private void ClickedButtonItems()
        {
            ItemSelected.Instance.Show();
        }

        private void ClickedDungeon()
        {
            //TODO: ������� ������ ���������� ����
            DungeonPage.Instance.Show(PlayerDungeonContainer.Instance.Dungeons[0]);
        }

        private void ClickedActorSelected()
        {
            ActorSelected.Instance.Show();
        }

        private void ClickedCommandCenter()
        {
            Debug.Log($"ClickedCommandCenter");
            CommandCenter.Instance.Show();
        }

        public void UpdateReputation(int value)
        {
            _reputationValueElement.text = value.ToString().ToPriceStyle();
        }

        public void LoadReputation()
        {
            UpdateReputation(PlayerReputation.GetReputation);
        }
    }
}