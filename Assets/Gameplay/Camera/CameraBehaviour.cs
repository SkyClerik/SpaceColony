using UnityEngine;

namespace Gameplay
{
    public class CameraBehavior : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1;
        [SerializeField]
        public VariableJoystick _variableJoystick;

        private Vector3 _direction;

        public void FixedUpdate()
        {
            if (_variableJoystick.Direction != Vector2.zero)
            {
                _direction = Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal;
                _direction.y = 0;
                transform.Translate(_direction * _speed * Time.fixedDeltaTime, Space.World);
            }
        }
    }
}