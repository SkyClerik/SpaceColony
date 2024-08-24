using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Gameplay.Data
{
    [CustomEditor(typeof(ItemDefinition))]
    public class ItemDefinitionEditor : Editor
    {
        private ItemDefinition _itemDefinition;

        private void OnEnable()
        {
            _itemDefinition = target as ItemDefinition;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            VisualElement element = new VisualElement();
            element.style.backgroundImage = new StyleBackground(_itemDefinition.Icon);
            element.style.width = 100 * _itemDefinition.SlotDimension.DefaultWidth;
            element.style.height = 100 * _itemDefinition.SlotDimension.DefaultHeight;
            root.Add(element);

            return root;
        }
    }
}