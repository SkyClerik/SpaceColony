using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Gameplay
{
    [RequireComponent(typeof(Volume))]
    public class GlobalVolumeManager : Singleton<GlobalVolumeManager>
    {
        [SerializeField]
        private Volume _volume;

        private DepthOfField _depthOfField;

        private void OnValidate()
        {
            _volume = GetComponent<Volume>();
        }

        public void SetActiveDepthOfField(bool active)
        {
            if (_depthOfField == null)
                _volume.profile.TryGet(out _depthOfField);

            _depthOfField.active = active;
        }
    }
}