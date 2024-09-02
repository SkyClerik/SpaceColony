using UnityEngine;

public class SelectedObjects : Singleton<SelectedObjects>
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private BuildingControl _buildingControl;
    [SerializeField]
    private LayerMask _layerMaskFromObjects;

    private CameraHit _cameraHit;
    private bool _active = false;

    public bool Active { get => _active; set => _active = value; }

    void Start()
    {
        _cameraHit = new CameraHit(_camera);
    }

    void Update()
    {
        if (!_active)
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
        foreach (BuildingBehavior item in _buildingControl.BuildingBehaviors)
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