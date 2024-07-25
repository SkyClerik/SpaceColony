using System;

namespace Assets.Inventory
{
    [Serializable]
    public class StoredItem
    {
        public ItemDefinition Details;
        public ItemVisual RootVisual;
        private PlayerInventory inventory;
        public PlayerInventory OwnerInventory
        {
            get => inventory;
            set => inventory = value;
        }
    }
}