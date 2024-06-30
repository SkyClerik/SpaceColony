using UnityEngine;

namespace Gameplay
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField]
        private Renderer _renderer;

        void OnEnable()
        {
            if (_renderer.isVisible)
                _renderer.enabled = false;
        }

        void LateUpdate()
        {
            var target = Camera.main.transform.position;
            target.y = transform.position.y;
            transform.LookAt(target);
        }

        void OnBecameVisible() => _renderer.enabled = true;

        void OnBecameInvisible() => _renderer.enabled = false;
    }
}