using System;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Data/Item")]
    public class ItemDefinition : ScriptableObject
    {
        public string ID = Guid.NewGuid().ToString();
        public string FriendlyName;
        public string Description;
        public int SellPrice;
        public int CurPCS;
        public int MaxPCS;
        public Sprite Icon;
        public ItemDimensions SlotDimension;

        private void OnValidate()
        {
            SlotDimension.CurrentHeight = SlotDimension.DefaultHeight;
            SlotDimension.CurrentWidth = SlotDimension.DefaultWidth;
        }
    }

    [Serializable]
    public struct InventoryDimensions
    {
        public int Height;
        public int Width;
    }

    [Serializable]
    public struct ItemDimensions
    {
        public int DefaultHeight;
        public int DefaultWidth;
        [HideInInspector]
        public float DefaulAngle;

        public int CurrentHeight { get; set; }
        public int CurrentWidth { get; set; }
        public float CurrentAngle { get; set; }
    }
}