#if UNITY_EDITOR
using UnityEngine;

public class GizmoBehaviour : Singleton<GizmoBehaviour>
{
    [SerializeField]
    private float _radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
#endif