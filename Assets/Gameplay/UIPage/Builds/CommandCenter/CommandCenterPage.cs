using Gameplay.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class CommandCenterPage : UIPage<CommandCenterPage>
    {
        private Button _buttonClose;
        private const string _buttonCloseName = "button_close";

        private VisualElement _row;
        private const string _rowName = "row";
        private ScrollView _scrollView;
        private const string _scrollViewName = "scroll_view";

        private BuildingControl _buildingControl;
        private List<BuildDrawing> _buildDrawings = new();
        private List<BuildDrawingTemplate> buildImageTemplates = new();

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            _buttonClose = rootElement.Q<Button>(_buttonCloseName);
            _buttonClose.clicked += ClickedButtonClose;

            _scrollView = rootElement.Q<ScrollView>(_scrollViewName);
            _row = rootElement.Q<VisualElement>(_rowName);
            _row.Clear();

            _buildingControl = BuildingControl.Instance;
            _buildDrawings = _buildingControl.GetDrawingList();

            for (int i = 0; i < _buildDrawings.Count; i++)
            {
                if (_buildDrawings == null)
                {
                    Debug.Log($"Пустая ссылка _buildDrawings в BuildingControl");
                    continue;
                }

                if (_buildDrawings[i].GetBuildingBehavior.GetBuildDefinition.HideFromPlayer)
                    continue;

                var templateObject = new BuildDrawingTemplate(
                    buildDrawing: _buildDrawings[i],
                    template: UserInterfaceShare.Instance.GetBuildDrawingTemplate,
                    root: _row,
                    callback: OnTemplateButtonClicked);
                buildImageTemplates.Add(templateObject);
            }
        }

        private void OnTemplateButtonClicked(BuildingBehavior buildingBehavior)
        {
            Hide();
            SelectedDruggedObjects.Instance.Active = true;
            _buildingControl.SelectShadowBuilding(buildingBehavior);
        }

        private void OnEnable()
        {
            UserInterfaceShare.Instance.OpenNewPage += IsOpenNewPage;
        }

        private void OnDisable()
        {
            UserInterfaceShare.Instance.OpenNewPage -= IsOpenNewPage;
        }

        private void IsOpenNewPage(UIDocument uIDocument)
        {
            if (uIDocument != document)
                Hide();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        private void ClickedButtonClose()
        {
            Hide();
        }
    }
}