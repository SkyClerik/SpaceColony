using Gameplay.Data;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class ResourceDefinitionTemplate : VisualElement
    {
        private string _resourceID;
        private VisualElement _icon;
        private Label _title;

        private const string _iconName = "icon";
        private const string _titleName = "title";

        public string GetResourceID => _resourceID;

        public ResourceDefinitionTemplate(VisualElement rootElement, ResourceDefinition resourceDefinition)
        {
            _resourceID = resourceDefinition.ID;
            TemplateContainer globalResource = UserInterfaceShare.Instance.GetGlobalResourceTemplate.Instantiate();
            _icon = globalResource.Q(_iconName);
            _icon.style.backgroundImage = new StyleBackground(resourceDefinition.Icon);
            _title = globalResource.Q<Label>(_titleName);

            Add(globalResource);
            rootElement.Add(this);

            SetText(resourceDefinition);
        }

        public void SetText(ResourceDefinition resourceDefinition)
        {
            _title.text = $"{resourceDefinition.CurPCS}";
        }
    }
}