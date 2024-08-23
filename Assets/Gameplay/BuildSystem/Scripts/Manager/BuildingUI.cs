using System.Collections.Generic;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    private BuildingControl _buildingControl;

    private bool _isMain;
    private Rect _mainArea;
    private float _shopButtonSize = 60;

    private Rect _shopArea;
    private List<BuildingBehaviour> _buildingList;

    public void Init(BuildingControl buildingControl, BuildingContainer buildingContainer)
    {
        var buttonOffset = (_shopButtonSize * 2);
        _mainArea = new Rect(Screen.width - buttonOffset, Screen.height - buttonOffset, _shopButtonSize, _shopButtonSize);
        _shopArea = new Rect(Screen.width - 200, Screen.height - 200, buttonOffset, buttonOffset);

        _buildingControl = buildingControl;
        _buildingList = buildingContainer.Buildings;
        _isMain = true;
    }

    private void OnGUI()
    {
        if (_isMain)
        {
            GUILayout.BeginArea(_mainArea);
            if (GUILayout.Button("Список"))
            {
                _isMain = !_isMain;
            }
            GUILayout.EndArea();
        }
        else
        {
            GUILayout.BeginArea(_shopArea);
            for (int i = 0; i < _buildingList.Count; i++)
            {

                if (GUILayout.Button($"{_buildingList[i].GetBuildInfo.Title}"))
                {
                    _isMain = !_isMain;
                    _buildingControl.SelectShadowBilding(i);
                }
            }
            GUILayout.EndArea();
        }
    }
}