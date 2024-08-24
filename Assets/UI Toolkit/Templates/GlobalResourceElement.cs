using Gameplay.Data;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class GlobalResourceElement : VisualElement
    {
        private VisualElement _icon;
        private Label _title;

        private const string _iconName = "icon";
        private const string _titleName = "title";

        public GlobalResourceElement(VisualElement rootElement, GlobalResource resource)
        {
            TemplateContainer globalResource = UserInterfaceShare.Instance.GlobalResourceTemplate.Instantiate();
            _icon = globalResource.Q(_iconName);
            _icon.style.backgroundImage = new StyleBackground(resource.Icon);
            _title = globalResource.Q<Label>(_titleName);
            _title.text = $"{resource.CurPCS}";
            Add(globalResource);

            rootElement.Add(this);
        }
    }
}