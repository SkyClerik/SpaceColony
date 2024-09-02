using Behavior;
using System;
using UnityEngine;

namespace Gameplay
{
    public class DungeonEvents : Singleton<DungeonEvents>
    {
        public Action<CarBehavior, Transform> CarTaskComplete;

        public void OnCarTaskComplete(CarBehavior carBehavior, Transform destination)
        {
            Debug.Log($"������ �������� {destination.name}", carBehavior.gameObject);
            CarTaskComplete?.Invoke(carBehavior, destination);
        }

        public Action<DungeonBehavior> QuestStarting;

        public void OnQuestStarting(DungeonBehavior dungeonBehavior)
        {
            Debug.Log($"������ ���������� �������");
            QuestStarting?.Invoke(dungeonBehavior);
        }

        public Action<DungeonBehavior> QuestComplete;

        public void OnQuestComplete(DungeonBehavior dungeonBehavior)
        {
            Debug.Log($"������� ���������");
            QuestComplete?.Invoke(dungeonBehavior);
        }

        public Action<DungeonBehavior> FarmComplete;

        public void OnFarmComplete(DungeonBehavior dungeonBehavior)
        {
            Debug.Log($"���� ��������");
            FarmComplete?.Invoke(dungeonBehavior);
        }
    }
}