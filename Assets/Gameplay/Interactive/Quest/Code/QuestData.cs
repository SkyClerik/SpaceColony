using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Data/QuestData")]
    public class QuestData : ScriptableObject
    {
        public string Title = "Title";
        public string PosTitle = "Pos Title";
        [TextArea]
        public string Description = "Description Text";

        public int AddReputation;
        public int RemoveReputation;

        public byte[] OnlyDates = new byte[30];

        public bool OnlyDay = false;
        public bool OnlyNight = false;

        public int HowMuchExperienceNeeded = 10;
        public int WhatBonusFromProfession = 10;

        public List<ActorType> ActorTypes;

        public Transform ParkingPosition { get; set; }

        public bool OnlyWin = false;
        public bool OnlyFail = false;

        public int ExpFromWin = 1;
    }
}