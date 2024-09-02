using System.Collections.Generic;
using System;

namespace ToolBoxSystem
{
    public class ToolBox : Singleton<ToolBox>
    {
        private Dictionary<Type, object> data = new Dictionary<Type, object>();

        public static void Add(object obj)
        {
            var add = obj;
            var manager = obj as ToolBoxSystem.ManagerBase;
            if (manager != null)
                add = Instantiate(manager);
            else return;

            Instance.data.Add(obj.GetType(), add);

            if (add is IAwake)
            {
                (add as IAwake).OnAwake();
            }
        }

        public static T Get<T>()
        {
            object resolve;
            Instance.data.TryGetValue(typeof(T), out resolve);
            return (T)resolve;
        }

        public static void ClearToolBox()
        {
            //синглтон выживает при переходе по сценам, необходимо очистить ссылки на менеджеры и желательно объекты.
            Instance.data.Clear();
        }
    }
}