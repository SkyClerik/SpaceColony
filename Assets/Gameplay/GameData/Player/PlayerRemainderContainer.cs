using Gameplay.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerRemainderContainer : Singleton<PlayerRemainderContainer>
    {
        [SerializeField]
        private List<ItemDefinition> _itemList = new List<ItemDefinition>();

        public void AddItemDefinition(ItemDefinition itemDefinition)
        {
            _itemList.Add(itemDefinition);
        }
    }
}