using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingContainer", menuName = "BuildSystem/BuildingContainer")]
public class BuildingContainer : ScriptableObject
{
    [SerializeField]
    private List<BuildingBehaviour> _buildings = new List<BuildingBehaviour>();
    public List<BuildingBehaviour> Buildings => _buildings;
}