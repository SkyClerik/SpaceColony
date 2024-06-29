using AvatarLogic;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnumBuilder", menuName = "EnumBuilder")]
public class EnumBuilder : ScriptableObject
{
    [SerializeField]
    private TextAsset _textAsset;

    [SerializeField]
    private List<StateBase> _states = new List<StateBase>();

    public TextAsset TextAsset => _textAsset;
    public List<StateBase> States => _states;
}