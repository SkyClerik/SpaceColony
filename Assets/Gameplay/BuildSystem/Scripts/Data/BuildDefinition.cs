using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "BuildInfo", menuName = "BuildSystem/BuildInfo")]
    public class BuildDefinition : ScriptableObject
    {
        [SerializeField]
        private bool _isMainBuild;
        [SerializeField]
        private string _title;
        [SerializeField]
        private string _description;
        [SerializeField]
        private BuildMaterialDefinition _materialDefinition;
        [SerializeField]
        private bool _hideFromPlayer;
        [SerializeField]
        private Sprite _icon;

        public bool IsMainBuild => _isMainBuild;
        public string Title => _title;
        public string Description => _description;
        public BuildMaterialDefinition MaterialDefinition => _materialDefinition;
        public bool HideFromPlayer => _hideFromPlayer;
        public Sprite Icon => _icon;
    }
}