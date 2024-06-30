using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace SkyClerikExt
{
    public static class TransformExt
    {
        public static void SetX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        public static void SetY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        public static void SetZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        public static void SetLocalX(this Transform transform, float x)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }

        public static void SetLocalY(this Transform transform, float y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }

        public static void SetLocalZ(this Transform transform, float z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        }

        /// <summary>
        /// Сброс localPosition и localRotation в нуль и localScale в 1
        /// </summary>
        public static void LocalReset(this Transform tr)
        {
            tr.localPosition = Vector3.zero;
            tr.localRotation = Quaternion.identity;
            tr.localScale = Vector3.one;
        }

        /// <summary>
        /// Копирование в текущий transform значений Position, Rotation, Scale, Parent из другого transform-а
        /// </summary>
        /// <param name="doCloneScale">при true выполняет копирование Scale</param>
        /// <param name="doCloneParent">при true значение Parent устанавливается таким же как у копируемого transform-а</param>
        public static void CloneTransform(this Transform tr, Transform from, bool doCloneScale = false, bool doCloneParent = false)
        {
            tr.position = from.position;
            tr.rotation = from.rotation;
            if (doCloneScale)
                tr.localScale = from.localScale;
            if (doCloneParent)
                tr.parent = from.parent;
        }

        /// <summary>
        /// Поиск объекта по всей дочерней иерархии текущего объекта
        /// </summary>
        /// <param name="excludeCurrentTransform">При true выполняет поиск только в дочерних объектах, не включая в результаты самого себя</param>
        public static Transform FindChildRecursive(this Transform obj, string name, bool excludeCurrentTransform = false)
        {
            if (excludeCurrentTransform)
                return obj.GetComponentsInChildren<Transform>().FirstOrDefault(tr => tr.name == name && obj != tr);
            else
                return obj.GetComponentsInChildren<Transform>().FirstOrDefault(tr => tr.name == name);
        }

        /// <summary>
        /// Поиск всех объектов с любым из переданных имен по всей дочерней иерархии текущего объекта
        /// </summary>
        /// <param name="excludeCurrentTransform">При true выполняет поиск только в дочерних объектах, не включая в результаты самого себя</param>
        public static Transform[] FindChildsRecursive(this Transform obj, bool excludeCurrentTransform = false, params string[] names)
        {
            if (excludeCurrentTransform)
                return obj.GetComponentsInChildren<Transform>().Where(tr => names.Contains(tr.name) && obj != tr).ToArray();
            else
                return obj.GetComponentsInChildren<Transform>().Where(tr => names.Contains(tr.name)).ToArray();
        }

        /// <summary>
        /// Удаляет все компоненты за исключением Transform из объекта и его потомков
        /// </summary>
        /// <param name="excludeTypes">Типы компонентов, которые не нужно удалять</param>
        public static void RemoveChildComponentsRecursive(this Transform obj, params Type[] excludeTypes)
        {
            var allComponents = obj.GetComponentsInChildren<Component>().Where(c => c.GetType() != typeof(Transform));
            IEnumerable<Component> components = null;
            if (excludeTypes != null)
                components = allComponents.Where(c => c.GetType().In(excludeTypes) == false);
            else
                components = allComponents;

            int count = components.Count();
            for (int i = 0; i < count; i++)
                GameObject.DestroyImmediate(components.ElementAt(i));
        }
    }
}