using UnityEngine;

namespace SkyClericExt
{
    public static class AvatarExtension
    {
        private const float _single = .01f;
        private const float _zero = 0f;

        public static void SetRotation(this MonoBehaviour ownerObject, Vector3 targetPosition)
        {
            var heading = targetPosition - ownerObject.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            direction.y = _zero;

            if (direction.sqrMagnitude > _single)
            {
                ownerObject.transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        public static void SetRotation(this MonoBehaviour ownerObject, Vector3 targetPosition, float angularSpeed)
        {
            var heading = targetPosition - ownerObject.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            direction.y = _zero;

            if (direction.sqrMagnitude > _single)
            {
                Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);
                ownerObject.transform.rotation = Quaternion.Lerp(ownerObject.transform.rotation, rot, angularSpeed * Time.deltaTime);
            }
        }

        public static void SetRotation(this GameObject ownerObject, Vector3 targetPosition, float angularSpeed)
        {
            var heading = targetPosition - ownerObject.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            direction.y = _zero;

            if (direction.sqrMagnitude > _single)
            {
                Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);
                ownerObject.transform.rotation = Quaternion.Lerp(ownerObject.transform.rotation, rot, angularSpeed * Time.deltaTime);
            }
        }
    }
}