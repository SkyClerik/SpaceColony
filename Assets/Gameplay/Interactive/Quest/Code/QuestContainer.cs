using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class QuestContainer : Singleton<QuestContainer>
    {
        [SerializeField]
        private List<Quest> _quests = new List<Quest>();

        [SerializeField]
        private List<QuestData> _allQuests = new List<QuestData>();

        public void SetQuests(List<Quest> quests) => _quests = quests;

        private void Start()
        {
            int start = Random.Range(0, _quests.Count);
            int repeat = Random.Range(0, _quests.Count);
            InvokeRepeating(nameof(AddQuestTo), start, repeat);
        }

        private void AddQuestTo()
        {
            if (_allQuests.Count > 0)
            {
                int r = Random.Range(0, _quests.Count);
                int a = Random.Range(0, _allQuests.Count);
                _quests[r].AddQuest(_allQuests[a]);
                _allQuests.RemoveAt(a);
            }
        }
    }
}