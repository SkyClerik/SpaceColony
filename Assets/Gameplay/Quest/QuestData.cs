using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Gameplay/Data/QuestData")]
    public class QuestData : ScriptableObject
    {
        public string Title;
        [TextArea]
        public string Description;

        public byte[] onlyDates = new byte[30];

        public bool _onlyDay = false;
        public bool _onlyNight = false;

        public Transform ParkingPosition { get; set; }
    }
}