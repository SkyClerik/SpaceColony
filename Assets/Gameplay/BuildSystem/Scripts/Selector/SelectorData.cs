using UnityEngine;

[CreateAssetMenu(fileName = "SelectorData", menuName = "BuildSystem/SelectorData")]
public class SelectorData : ScriptableObject
{
    [SerializeField]
    private Material _blueColor;
    [SerializeField]
    private Material _redColor;
    [SerializeField]
    private Material _whiteColor;

    public Material BlueColor { get => _blueColor; set => _blueColor = value; }
    public Material RedColor { get => _redColor; set => _redColor = value; }
    public Material WhiteColor { get => _whiteColor; set => _whiteColor = value; }
}