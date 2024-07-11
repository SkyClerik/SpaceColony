using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "QuestDataLibrary", menuName = "Data/QuestDataLibrary")]
    public class QuestDataLibrary : ScriptableObject
    {
        [SerializeField]
        private List<QuestData> questDatas = new List<QuestData>();
    }
}