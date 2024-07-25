using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Gameplay/Data/QuestData")]
    public class QuestData : ScriptableObject
    {
        public string Title;
        [TextArea]
        public string Description;
        public int AddReputation;
        public int RemoveReputation;

        public byte[] OnlyDates = new byte[30];

        public bool OnlyDay = false;
        public bool OnlyNight = false;

        public int HowMuchExperienceNeeded = 10;
        public int WhatBonusFromProfession = 10;

        public List<ActorType> ActorTypes;

        public Transform ParkingPosition { get; set; }
    }
}