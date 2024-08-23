using System.Collections.Generic;
using UnityEngine;
using QuestSystem;

namespace Gameplay
{
    public class QuestContainer : Singleton<QuestContainer>
    {
        [SerializeField]
        private List<Quest> _quests = new List<Quest>();

        [SerializeField]
        private List<QuestData> _allQuests = new List<QuestData>();

        public void SetQuests(List<Quest> quests) => _quests = quests;

        private const int _five = 5;
        private const int _thirty = 5;

        private void Start()
        {
            for (int i = 0; i < _allQuests.Count; i++)
            {
                _allQuests[i] = Instantiate(_allQuests[i]);
            }

            int start = Random.Range(_five, _quests.Count + _five);
            int repeat = Random.Range(_thirty, _quests.Count + _thirty);
            InvokeRepeating(nameof(AddQuestTo), start, repeat);
        }

        private void AddQuestTo()
        {
            if (_allQuests.Count > 0)
            {
                int r = Random.Range(0, _quests.Count);
                int a = Random.Range(0, _allQuests.Count);

                if (_quests[r].GetQuestData == null)
                {
                    _quests[r].AddQuest(_allQuests[a]);

                    if (!_allQuests[a].Repeatable)
                        _allQuests.RemoveAt(a);
                    else
                        _allQuests[a].NumberImpressions++;
                }
            }
        }
    }
}