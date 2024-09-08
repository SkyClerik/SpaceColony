using UnityEngine;

public class CameraHit
{
    private Camera _camera;

    public CameraHit(Camera camera = null)
    {
        _camera = camera == null ? Camera.main : camera;
    }

    public Vector3 GetHitFromScreenPointToRay(LayerMask layerMask)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            return hit.point;

        return Vector3.zero;
    }

    public Vector3Int GetPoint(LayerMask layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            return Vector3Int.RoundToInt(hit.point);

        return Vector3Int.zero;
    }

    public BoxCollider GetCollider(LayerMask layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            return hit.collider.GetComponent<BoxCollider>();

        return null;
    }

    public Vector3 GetPointCenter(LayerMask layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            return hit.transform.position;

        return Vector3.zero;
    }

    private GameObject RayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            return hit.collider.gameObject;

        return null;
    }
}