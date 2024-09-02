using Gameplay.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerItemsContainer : Singleton<PlayerItemsContainer>
    {
        [SerializeField]
        private List<ItemDefinition> _items = new List<ItemDefinition>();
        public List<ItemDefinition> Items { get => _items; set => _items = value; }

        private void Awake()
        {
            Cloning();
        }

        private void Cloning()
        {
            for (int i = 0; i < _items.Count; i++)
                _items[i] = Instantiate(Items[i]);
        }

        public void Add(TrophyItem trophyItem)
        {
            if (TryFindItemByTrophy(trophyItem, out ItemDefinition item))
            {
                item.PlusPCS(trophyItem);
                return;
            }
            else
            {
                var newItem = Instantiate(trophyItem.Item);
                newItem.PlusPCS(trophyItem);
                _items.Add(newItem);
            }
        }

        public void Remove(ItemDefinition itemDefinition)
        {
            _items.Remove(itemDefinition);
        }

        public ItemDefinition Get(int index)
        {
            return _items[index];
        }

        public bool TryFindItemByTrophy(TrophyItem trophyItem, out ItemDefinition itemDefinition)
        {
            foreach (var item in _items)
            {
                if (item.ID.Equals(trophyItem.Item.ID))
                {
                    itemDefinition = item;
                    return true;
                }
            }
            itemDefinition = null;
            return false;
        }
    }
}
