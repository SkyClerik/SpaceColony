using Gameplay.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class BuildingControl : Singleton<BuildingControl>
    {
        [SerializeField]
        private BuildDrawingContainer _buildDrawingContainer;
        [SerializeField]
        private LayerMask _floorLayerMask;
        [SerializeField]
        private int _greedSize = 1;

        public LayerMask FloorLayerMask => _floorLayerMask;
        public int GreedSize => _greedSize;

        private void Start()
        {
            if (_buildDrawingContainer == null)
            {
                Debug.LogError("Не назначена база данных с объектами строительства", gameObject);
                return;
            }

            _buildDrawingContainer = Instantiate(_buildDrawingContainer);
        }

        public void SelectShadowBuilding(BuildingBehavior buildingBehavior)
        {
            GameObject shadowBuild = Instantiate(buildingBehavior.gameObject);

            Rigidbody rigidbody = shadowBuild.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;

            BuildDragger buildDragger = shadowBuild.AddComponent<BuildDragger>();
            buildDragger.Init(DraggerTypes.Create);
        }

        public List<BuildDrawing> GetDrawingList()
        {
            return _buildDrawingContainer.Buildings;
        }
    }
}