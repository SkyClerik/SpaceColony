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

        public static void AddTo(object updateble)
        {
            var mngUpdate = ToolBox.Get<ToolBoxSystem.Update>();

            if (updateble is ITick)
                mngUpdate.ticks.Add(updateble as ITick);

            if (updateble is ITickFixed)
                mngUpdate.ticksFixed.Add(updateble as ITickFixed);

            if (updateble is ITickLate)
                mngUpdate.ticksLate.Add(updateble as ITickLate);
        }

        public static void RemoveFrom(object updateble)
        {
            var mngUpdate = ToolBox.Get<ToolBoxSystem.Update>();

            if (updateble is ITick)
                mngUpdate.ticks.Remove(updateble as ITick);

            if (updateble is ITickFixed)
                mngUpdate.ticksFixed.Remove(updateble as ITickFixed);

            if (updateble is ITickLate)
                mngUpdate.ticksLate.Remove(updateble as ITickLate);
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