using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingContainer", menuName = "BuildSystem/BuildingContainer")]
public class BuildingContainer : ScriptableObject
{
    [SerializeField]
    private List<BuildingBehavior> _buildings = new List<BuildingBehavior>();
    public List<BuildingBehavior> Buildings => _buildings;
}