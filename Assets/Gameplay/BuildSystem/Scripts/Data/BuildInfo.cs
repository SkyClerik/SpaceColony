using UnityEngine;

[CreateAssetMenu(fileName = "BuildInfo", menuName = "BuildSystem/BuildInfo")]
public class BuildInfo : ScriptableObject
{
    [SerializeField]
    private string _title;
    [SerializeField]
    private Material _defaultMaterial;
    [SerializeField]
    private Material _drugedMaterial;


    public string Title => _title;
    public Material GetDefaultMaterial => _defaultMaterial;
    public Material GetDrugedMaterial => _drugedMaterial;
}