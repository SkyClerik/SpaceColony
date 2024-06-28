using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Gameplay/Data/QuestData")]
    public class QuestData : ScriptableObject
    {
        [SerializeField]
        private string _description;
    }
}