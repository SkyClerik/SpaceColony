using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "BuildingContainer", menuName = "BuildSystem/BuildingContainer")]
    public class BuildDrawingContainer : ScriptableObject
    {
        [SerializeField]
        private List<BuildDrawing> _buildings = new List<BuildDrawing>();
        public List<BuildDrawing> Buildings => _buildings;
    }

    [System.Serializable]
    public class BuildDrawing
    {
        [SerializeField]
        public BuildingBehavior _buildingBehavior;
        [SerializeField]
        public byte _curCount = 0;
        [SerializeField]
        public byte _maxCount = 1;

        public BuildingBehavior GetBuildingBehavior => _buildingBehavior;

        public bool IsMaxCount
        {
            get
            {
                if (_curCount == _maxCount)
                    return true;

                return false;
            }
        }
    }
}