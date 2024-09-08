using UnityEngine;

namespace Gameplay
{
    public class SelectedDruggedObjects : Singleton<SelectedDruggedObjects>
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private LayerMask _layerMaskFromObjects;
        [SerializeField]
        private bool _activeBuildingSystem = false;

        private CameraHit _cameraHit;

        public bool Active { get => _activeBuildingSystem; set => _activeBuildingSystem = value; }

        private void Start()
        {
            _cameraHit = new CameraHit(_camera);
        }

        private void Update()
        {
            if (!_activeBuildingSystem)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                BoxCollider point = _cameraHit.GetCollider(_layerMaskFromObjects);
                if (point != null)
                {
                    if (TryFindObject(point, out GameObject obj))
                    {
                        obj.AddComponent<BuildDragger>().Init(DraggerTypes.Move);
                    }
                }
            }
        }

        private bool TryFindObject(BoxCollider boxCollider, out GameObject obj)
        {
            PlayerBuildsContainer playerBuildsContainer = PlayerBuildsContainer.Instance;
            foreach (BuildingBehavior item in playerBuildsContainer.BuildBoxOnHand)
            {
                if (item == null)
                    continue;

                var collider = item.GetComponent<BoxCollider>();
                if (boxCollider.Equals(collider))
                {
                    obj = item.gameObject;
                    return true;
                }
            }
            obj = null;
            return false;
        }
    }
}