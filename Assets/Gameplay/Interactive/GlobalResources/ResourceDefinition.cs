using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "New ResourceDefinition", menuName = "Gameplay/Data/ResourceDefinition")]
    public class ResourceDefinition : ItemDefinition
    {
       
    }

    [System.Serializable]
    public class TrophyResource
    {
        public ResourceDefinition Resource;
        public IntMM PCS;
    }
}