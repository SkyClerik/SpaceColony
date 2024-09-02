using System.Collections.Generic;
using UnityEngine;

namespace ToolBoxSystem
{
    [CreateAssetMenu(fileName = "ManagerTime", menuName = "ToolBoxSystem/ManagerTime")]
    public class Time : ManagerBase, IAwake, ITickFixed
    {
        private System.TimeSpan _gameTime = new System.TimeSpan(0, 0, 0, 0);   //  https://www.youtube.com/watch?v=YNwHmbinus0
        private float _frame = 0f;
        public List<IRPS> IRPSObjects = new List<IRPS>();

        public void OnAwake()
        {
            Update.AddTo(this);
        }

        public void TickFixed()
        {
            //  Для запуска каждую секунду
            _frame += UnityEngine.Time.deltaTime;
            if (_frame >= 1)
            {
                _frame = 0;
                _gameTime = _gameTime.Add(System.TimeSpan.FromSeconds(1));
                PerSecond();
            }
        }
        void PerSecond()
        {
            for (int i = 0; i < IRPSObjects.Count; i++)
                IRPSObjects[i].RPS();
        }

        //string TimeNormalize(float seconds)
        //{
        //    int sec_s = (int)seconds;
        //    int hour = (sec_s / 3600);
        //    int min = (sec_s - hour * 3600) / 60;
        //    int sec = (sec_s - hour * 3600 - min * 60);
        //    string time = min.ToString() + " : " + sec.ToString();
        //    return time;
        //}

        public string GetTime()
        {
            return _gameTime.ToString();
        }

        public void AddIRPSObjects(IRPS extObj)
        {
            if (!string.IsNullOrEmpty(extObj.Identificatory))
            {
                foreach (var item in IRPSObjects)
                {
                    if (item.Identificatory.Equals(extObj.Identificatory))
                    {
                        Debug.Log("Такой ID уже используется", extObj as MonoBehaviour);
                        return;
                    }
                }
            }
            else
            {
                extObj.Identificatory = GetID();
            }

            IRPSObjects.Add(extObj);
        }
        public void RemoveIRPSObjects(IRPS extObj)
        {
            foreach (var item in IRPSObjects)
            {
                if (item.Identificatory == extObj.Identificatory)
                {
                    IRPSObjects.Remove(item);
                    return;
                }
            }
        }

        string GetID()
        {
            Debug.Log("Выдаю новый ID для подписчика таймера");
            return System.Guid.NewGuid().ToString();
        }
    }
}