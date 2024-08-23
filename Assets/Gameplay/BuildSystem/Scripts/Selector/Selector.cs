using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Selector : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private SelectorData _selectorData;

    private void OnValidate()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetActive(bool enable)
    {
        gameObject.SetActive(enable);
    }

    public void SetColorBlue()
    {
        _meshRenderer.material = _selectorData.BlueColor;
    }
    public void SetColorRed() => _meshRenderer.material = _selectorData.RedColor;
    public void SetColorWhite() => _meshRenderer.material = _selectorData.WhiteColor;
}