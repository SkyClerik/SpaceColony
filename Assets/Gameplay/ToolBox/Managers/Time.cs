using System.Collections.Generic;
using UnityEngine;

namespace ToolBoxSystem
{
    [CreateAssetMenu(fileName = "ManagerTime", menuName = "ToolBoxSystem/ManagerTime")]
    public class Time : ManagerBase, IAwake, ITickFixed
    {
        System.TimeSpan gameTime = new System.TimeSpan(0, 0, 0, 0);   //  https://www.youtube.com/watch?v=YNwHmbinus0
        float frame = 0f;
        public List<IRPS> IRPSObjects = new List<IRPS>();
        private int maxIdentificator = 0;

        public void OnAwake()
        {
            Update.AddTo(this);
        }

        public void TickFixed()
        {
            //  Для запуска каждую секунду
            frame += UnityEngine.Time.deltaTime;
            if (frame >= 1)
            {
                frame = 0;
                gameTime = gameTime.Add(System.TimeSpan.FromSeconds(1));
                PerSecond();
            }
        }
        void PerSecond()
        {
            for (int i = 0; i < IRPSObjects.Count; i++)
            {
                IRPSObjects[i].RPS();
            }
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
            return gameTime.ToString();
        }

        public void AddIRPSObjects(IRPS extObj)
        {
            if (extObj.Identificator != "")
            {
                foreach (var item in IRPSObjects)
                {
                    if (item.Identificator == extObj.Identificator)
                    {
                        Debug.Log("Такой ID уже мспользуется", extObj as MonoBehaviour);
                        return;
                    }
                }
            }
            else
            {
                extObj.Identificator = GetID();
            }
            IRPSObjects.Add(extObj);
        }
        public void RemoveIRPSObjects(IRPS extObj)
        {
            foreach (var item in IRPSObjects)
            {
                if (item.Identificator == extObj.Identificator)
                {
                    IRPSObjects.Remove(item);
                    return;
                }
            }
        }

        string GetID()
        {
            Debug.Log("Выдаю новый ID для подписчика таймера");
            maxIdentificator++;
            return (maxIdentificator - 1).ToString();
        }

    }
}