#if UNITY_EDITOR
using UnityEngine;

public class GizmoPoint : MonoBehaviour
{
    [SerializeField]
    private Vector3 _size = Vector3.one;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}
#endif