using UnityEngine;

namespace SkyClerikExt
{
    public static class AvatarExtension
    {
        public static void SetRotation(this MonoBehaviour ownerObject, Vector3 targetPosition)
        {
            var heading = targetPosition - ownerObject.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            direction.y = 0;
            ownerObject.transform.rotation = Quaternion.LookRotation(direction);
        }

        public static void SetRotation(this MonoBehaviour ownerObject, Vector3 targetPosition, float angularSpeed)
        {
            var heading = targetPosition - ownerObject.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            direction.y = 0;
            Quaternion rot = Quaternion.LookRotation(direction);
            ownerObject.transform.rotation = Quaternion.Lerp(ownerObject.transform.rotation, rot, angularSpeed * Time.deltaTime);
        }
    }
}