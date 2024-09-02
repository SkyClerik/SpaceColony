using Gameplay.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [System.Serializable]
    public class LootInfo
    {
        [SerializeField]
        private List<TrophyResource> _resources = new List<TrophyResource>();
        [SerializeField]
        private List<TrophyItem> _items = new List<TrophyItem>();
        [SerializeField]
        private List<TrophyActor> _actors = new List<TrophyActor>();
        [SerializeField]
        private List<TrophyPet> _pets = new List<TrophyPet>();
        [SerializeField]
        private List<TrophyDrawing> _drawings = new List<TrophyDrawing>();


        public List<TrophyResource> Resources => _resources;
        public List<TrophyItem> Items => _items;
        public List<TrophyActor> Actors => _actors;
        public List<TrophyPet> Pets => _pets;
        public List<TrophyDrawing> Drawings => _drawings;

        //TODO: Не забудь про питомцев, чертежи, и прочее
    }
}