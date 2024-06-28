using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class QuestContainer : Singleton<QuestContainer>
    {
        [SerializeField]
        private List<Quest> _quests = new List<Quest>();
    }
}