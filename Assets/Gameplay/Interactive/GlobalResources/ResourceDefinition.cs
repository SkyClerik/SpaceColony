using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "New ResourceDefinition", menuName = "Gameplay/Data/ResourceDefinition")]
    public class ResourceDefinition : ItemDefinition
    {
        public void PlusPCS(TrophyResource trophyResource)
        {
            var value = CurPCS + trophyResource.GetPCS;
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
                var result = CurPCS + trophyResource.GetPCS;
                Debug.Log($"Просто добавляем {result}");
                CurPCS = result;
                return;
            }
        }

        public void MinusPCS()
        {

        }
    }

    [System.Serializable]
    public class TrophyResource
    {
        [SerializeField]
        public ResourceDefinition Resource;
        [SerializeField]
        private IntMM _pcs;
        private int _resultPCS;

        public int GetPCS => _resultPCS = _pcs.OnlyMinValue ? _pcs.MinValue : UnityEngine.Random.Range(_pcs.MinValue, _pcs.MaxValue);
    }
}