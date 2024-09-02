using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace SkyClericExt
{
    public static partial class Other
    {
        /// <summary>
        /// Возвращает случайный объект из листа, того от которого вызывается.
        /// </summary>
        public static T Random<T>(this List<T> list)
        {
            var val = list[UnityEngine.Random.Range(0, list.Count)];
            return val;
        }

        /// <summary>
        /// Увеличивает на передаваемое значение переменную по имени в принимаемом типе данных
        /// </summary>
        public static T SetField<T>(T _type, string name, float value)
        {
            FieldInfo[] myField = _type.GetType().GetFields();

            for (int i = 0; i < myField.Length; i++)
            {
                if (myField[i].Name.ToLower() == name.ToLower())
                {
                    switch (myField[i].GetValue(_type))
                    {
                        case int a:
                            myField[i].SetValueDirect(__makeref(_type), a + value);
                            break;
                        case float a:
                            myField[i].SetValueDirect(__makeref(_type), a + value);
                            break;
                    }
                }
            }
            return _type;
        }

        public interface IChance
        {
            float returnChance { get; set; }
        }

        /// <summary>
        /// Возвращает рандомно объект листа основываясь на указанном в типе значении шанса
        /// </summary>
        public static T RandomByChance<T>(this List<T> obj) where T : IChance
        {
            var total = 0f;
            var probs = new float[obj.Count];

            for (int i = 0; i < probs.Length; i++)
            {
                probs[i] = obj[i].returnChance;
                total += probs[i];
            }
            System.Random _r = new System.Random();
            var randomPoint = (float)_r.NextDouble() * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                    return obj[i];
                randomPoint -= probs[i];
            }
            return obj[0];
        }

        /// <summary>
        ///  Возвращает пустую ячейку листа
        /// <summary>
        public static bool GetClearCall<T>(this T[] array, out int num)
        {
            num = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                    num = i;
            }
            return false;
        }

        /// <summary>
        /// Возвращает новый созданный идентификатор
        /// </summary>
        public static string GetNewID()
        {
            Guid g = Guid.NewGuid();
            return g.ToString();
        }

        /// <summary>
        /// Возвращает расстояние от объекта до объекта
        /// </summary>
        public static float GetDistance(Transform ATransform, Transform BTransform)
        {
            Vector3 AVector = ATransform.position;
            Vector3 BVector = BTransform.position;

            return Vector3.Distance(new Vector2(AVector.x, AVector.z), new Vector2(BVector.x, BVector.z));
        }
    }
}
