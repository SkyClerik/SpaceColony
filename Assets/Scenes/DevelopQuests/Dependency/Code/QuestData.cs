using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Data/QuestData")]
    public class QuestData : ScriptableObject
    {
        [SerializeField]
        private string _title = "Title";
        [SerializeField]
        private string _posTitle = "Pos Title";
        [SerializeField]
        private string _description = "Description Text";
        public string Description { get { return _description; } set { _description = value; } }
    }
}