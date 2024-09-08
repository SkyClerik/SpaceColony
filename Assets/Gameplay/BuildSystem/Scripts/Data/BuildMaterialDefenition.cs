using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "BuildMaterialDefinition", menuName = "BuildSystem/BuildMaterialDefinition")]
    public class BuildMaterialDefinition : ScriptableObject
    {
        [SerializeField]
        private Material _defaultMaterial;
        [SerializeField]
        private Material _draggedMaterial;

        public Material GetDefaultMaterial => _defaultMaterial;
        public Material GetDraggedMaterial => _draggedMaterial;
    }
}