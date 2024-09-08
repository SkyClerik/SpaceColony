using Gameplay.Data;
using System;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class BuildDrawingTemplate : VisualElement
    {
        private Label _labelMainTitle;
        private const string _labelMainTitleName = "label_main_title";
        private Label _labelDescription;
        private const string _labelDescriptionName = "label_description";
        private VisualElement _mainImage;
        private const string _mainImageName = "main_image";
        private Button _buttonApply;
        private const string _buttonApplyName = "button_apply";

        private BuildDrawing _buildDrawing;
        private BuildingBehavior _buildingBehavior;
        private BuildDefinition _buildDefinition;
        private Action<BuildingBehavior> _callback;

        public BuildDrawingTemplate(BuildDrawing buildDrawing, VisualTreeAsset template, VisualElement root, Action<BuildingBehavior> callback)
        {
            template.CloneTree(this);
            root.Add(this);

            _buildDrawing = buildDrawing;
            _buildingBehavior = _buildDrawing.GetBuildingBehavior;
            _buildDefinition = _buildingBehavior.GetBuildDefinition;
            _callback = callback;

            _labelMainTitle = this.Q<Label>(_labelMainTitleName);
            _labelMainTitle.text = _buildDefinition.Title;

            _labelDescription = this.Q<Label>(_labelDescriptionName);
            _labelDescription.text = _buildDefinition.Description;

            _mainImage = this.Q(_mainImageName);
            _mainImage.style.backgroundImage = new StyleBackground(_buildDefinition.Icon);

            _buttonApply = this.Q<Button>(_buttonApplyName);
            _buttonApply.clicked += ClickedButtonApply;

            if (buildDrawing.IsMaxCount)
                _buttonApply.SetEnabled(false);
        }

        private void ClickedButtonApply()
        {
            _callback?.Invoke(_buildingBehavior);
        }

        ~BuildDrawingTemplate()
        {
            _callback = null;
        }
    }
}