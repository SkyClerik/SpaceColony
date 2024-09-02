using UnityEngine;

[CreateAssetMenu(fileName = "BuildInfo", menuName = "BuildSystem/BuildInfo")]
public class BuildInfo : ScriptableObject
{
    [SerializeField]
    private string _title;
    [SerializeField]
    private Material _defaultMaterial;
    [SerializeField]
    private Material _draggedMaterial;
    [SerializeField]
    public int _curPCS;
    [SerializeField]
    public int _maxPCS;

    public string Title => _title;
    public Material GetDefaultMaterial => _defaultMaterial;
    public Material GetDraggedMaterial => _draggedMaterial;
    public int CurPCS { get => _curPCS; set => _curPCS = value; }
    public int MaxPCS { get => _maxPCS; set => _maxPCS = value; }
}