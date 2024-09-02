using System.Collections.Generic;
using UnityEngine;

namespace ToolBoxSystem
{
    [CreateAssetMenu(fileName = "ManagerUpdate", menuName = "ToolBoxSystem/ManagerUpdate")]
    public class Update : ManagerBase, IAwake
    {
        private List<ITick> ticks = new List<ITick>();
        private List<ITickFixed> ticksFixed = new List<ITickFixed>();
        private List<ITickLate> ticksLate = new List<ITickLate>();

        public static void AddTo(object updatable)
        {
            var mngUpdate = ToolBox.Get<ToolBoxSystem.Update>();

            if (updatable is ITick)
                mngUpdate.ticks.Add(updatable as ITick);

            if (updatable is ITickFixed)
                mngUpdate.ticksFixed.Add(updatable as ITickFixed);

            if (updatable is ITickLate)
                mngUpdate.ticksLate.Add(updatable as ITickLate);
        }

        public static void RemoveFrom(object updatable)
        {
            var mngUpdate = ToolBox.Get<ToolBoxSystem.Update>();

            if (updatable is ITick)
                mngUpdate.ticks.Remove(updatable as ITick);

            if (updatable is ITickFixed)
                mngUpdate.ticksFixed.Remove(updatable as ITickFixed);

            if (updatable is ITickLate)
                mngUpdate.ticksLate.Remove(updatable as ITickLate);
        }

        public void Tick()
        {
            for (int i = 0; i < ticks.Count; i++)
                ticks[i].Tick();
        }

        public void TickFixed()
        {
            for (int i = 0; i < ticksFixed.Count; i++)
                ticksFixed[i].TickFixed();
        }

        public void TickLate()
        {
            for (int i = 0; i < ticksLate.Count; i++)
                ticksLate[i].TickLate();
        }

        public void OnAwake()
        {
            ToolBox.Instance.gameObject.AddComponent<ToolBoxSystem.UpdateComponent>().Setup(this);
        }
    }
}