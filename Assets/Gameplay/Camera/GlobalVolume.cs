using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class GlobalVolume : MonoBehaviour
{
    [SerializeField]
    private Volume _volume;

    private DepthOfField _depthOfField;

    private void OnValidate()
    {
        _volume = GetComponent<Volume>();
    }

    public void TryEnableDepthOfField()
    {
        if (_volume.profile.TryGet(out _depthOfField))
        {
            _depthOfField.active = true;
        }
    }
}
