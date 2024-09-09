using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using SkyClericExt;
using Gameplay.Data;

namespace Gameplay.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class HUDUserInterface : HUDUserInterfaceFields
    {
        private List<ResourceDefinitionTemplate> _resourceDefinitionTemplates = new List<ResourceDefinitionTemplate>();

        private void OnEnable()
        {
            PlayerReputation.ReputationChange += UpdateReputation;
            PlayerGlobalResourcesContainer.Instance.OnResourcesChange += RepaintResource;
        }

        private void OnDestroy()
        {
            PlayerReputation.ReputationChange -= UpdateReputation;
            PlayerGlobalResourcesContainer.Instance.OnResourcesChange -= RepaintResource;
        }

        protected override void Awake()
        {
            base.Awake();
            base.Init();

            buttonCommandCenter.clicked += ClickedCommandCenter;
            buttonMining.clicked += ClickedButtonMining;
            buttonActorSelected.clicked += ClickedActorSelected;
            buttonItems.clicked += ClickedButtonItems;
            buttonDungeon.clicked += ClickedDungeon;
            buttonBuildType.clicked += ClickedBuildingMode;

            _resourceDefinitionTemplates = new List<ResourceDefinitionTemplate>();
            foreach (var resource in PlayerGlobalResourcesContainer.Instance.GetGlobalResources)
            {
                var newResource = new ResourceDefinitionTemplate(globalResourcesRoot, resource);
                _resourceDefinitionTemplates.Add(newResource);
            }

            LoadReputation();
        }

        private void ClickedBuildingMode()
        {
            SelectedDruggedObjects selectedDruggedObjects = SelectedDruggedObjects.Instance;
            selectedDruggedObjects.Active = !selectedDruggedObjects.Active;
            buttonBuildType.text = $"{buttonBuildTypeText}:\n{selectedDruggedObjects.Active}";
        }

        private void ClickedButtonMining()
        {
            //TODO: Вызываю первый попавшийся добытчик для тестов
            MiningBehavior miningBehavior = PlayerBuildsContainer.Instance.Find<MiningBehavior>();
            MiningPage.Instance.Show(miningBehavior);
        }

        private void ClickedButtonItems()
        {
            ItemSelected.Instance.Show();
        }

        private void ClickedDungeon()
        {
            //TODO: Вызываю первый попавшийся данж для тестов
            PlayerDungeonContainer.Instance.Dungeons[0].SystemClicked();
        }

        private void ClickedActorSelected()
        {
            ActorPage.Instance.Show();
        }

        private void ClickedCommandCenter()
        {
            CommandCenterPage.Instance.Show();
        }

        private void RepaintResource(ResourceDefinition resourceDefinition)
        {
            foreach (var item in _resourceDefinitionTemplates)
            {
                if (resourceDefinition.ID == item.GetResourceID)
                    item.SetText(resourceDefinition);
            }
        }

        private void UpdateReputation(int value)
        {
            reputationValueElement.text = value.ToString().ToPriceStyle();
        }

        private void LoadReputation()
        {
            UpdateReputation(PlayerReputation.GetReputation);
        }
    }
}