using Gameplay.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInventoriesContainer : Singleton<PlayerInventoriesContainer>
    {
        [SerializeField]
        private List<PlayerInventory> _inventories = new List<PlayerInventory>();
        public List<PlayerInventory> Inventories { get => _inventories; set => _inventories = value; }
    }
}