using System;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "New ItemDefinition", menuName = "Gameplay/Data/ItemDefinition")]
    public class ItemDefinition : ObjectBase
    {
        public string ID = Guid.NewGuid().ToString();
        public int CurPCS;
        public ItemDimensions SlotDimension;
        [SerializeField]
        private Coast _coast;

        public Coast Coast => _coast;

        private void OnValidate()
        {
            SlotDimension.CurrentHeight = SlotDimension.DefaultHeight;
            SlotDimension.CurrentWidth = SlotDimension.DefaultWidth;
        }

        public void PlusPCS(TrophyItem trophyItem)
        {
            var value = CurPCS + trophyItem.GetPCS;
            if (value > MaxPCS)
            {
                Debug.Log($"Есть избыток {this.name}: CurPCS:{CurPCS} - MaxPCS:{MaxPCS}");
                CurPCS = MaxPCS;

                var remainder = Instantiate(this);
                remainder.CurPCS = value - MaxPCS;
                PlayerRemainderContainer.Instance.AddItemDefinition(remainder);
                return;
            }
            else if (value == MaxPCS)
            {
                CurPCS = MaxPCS;
                return;
            }
            else if (value < MaxPCS)
            {
                var result = CurPCS + trophyItem.GetPCS;
                Debug.Log($"Просто добавляем {result}");
                CurPCS = result;
                return;
            }
        }

        public void MinusPCS()
        {

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
        public float DefaultAngle;

        public int CurrentHeight { get; set; }
        public int CurrentWidth { get; set; }
        public float CurrentAngle { get; set; }
    }

    [System.Serializable]
    public class TrophyItem
    {
        [SerializeField]
        private ItemDefinition _item;

        [SerializeField]
        private MinMaxValues _pcs;
        private int _resultPCS;

        [Range(0f, 100f), SerializeField]
        private byte _chance;

        public ItemDefinition Item => _item;
        public int GetPCS => _resultPCS = _pcs.OnlyMinValue ? _pcs.MinValue : UnityEngine.Random.Range(_pcs.MinValue, _pcs.MaxValue);
        public byte Chance => _chance;
    }
}