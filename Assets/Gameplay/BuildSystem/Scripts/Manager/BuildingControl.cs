using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingUI))]
public class BuildingControl : Singleton<BuildingControl>
{
    [SerializeField]
    private BuildingContainer _buildingContainer;
    [SerializeField]
    private LayerMask _floorLayerMask;
    [SerializeField]
    private List<BuildingBehavior> _buildingBehaviors = new();

    public LayerMask FloorLayerMask => _floorLayerMask;
    public int GreedSize = 1;
    public List<BuildingBehavior> BuildingBehaviors => _buildingBehaviors;

    private void Start()
    {
        if (_buildingContainer == null)
        {
            Debug.LogError("Не назначена база данных с объектами строительства", gameObject);
            return;
        }

        BuildingUI buildingUI = GetComponent<BuildingUI>();
        buildingUI.Init(this, _buildingContainer);
    }

    public void SelectShadowBuilding(int i)
    {
        GameObject shadowBuild = Instantiate(_buildingContainer.Buildings[i].gameObject);

        Rigidbody rigidbody = shadowBuild.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        BuildDragger buildDruger = shadowBuild.AddComponent<BuildDragger>();
        buildDruger.Init(DraggerTypes.Create);
    }

    public void TryAddBuildingBehavior(BuildingBehavior buildingBehavior)
    {
        foreach (BuildingBehavior item in _buildingBehaviors)
        {
            if (item.Equals(buildingBehavior))
                return;
        }

        for (int i = 0; i < _buildingBehaviors.Count; i++)
        {
            if (_buildingBehaviors[i] == null)
            {
                _buildingBehaviors[i] = buildingBehavior;
                return;
            }
        }
        _buildingBehaviors.Add(buildingBehavior);
    }
}