using UnityEngine;

namespace SkyClericExt
{
    public static class ComponentExt
    {
        public static I GetInterfaceComponent<I>(this Component comp) where I : class
        {
            return comp.GetComponent(typeof(I)) as I;
        }

        public static I[] GetInterfaceComponents<I>(this Component comp) where I : class
        {
            var components = comp.GetComponents(typeof(I));
            I[] iComponents = new I[components.Length];
            components.CopyTo(iComponents, 0);
            return iComponents;
        }

        public static T GetSafeComponent<T>(this Component comp) where T : Component
        {
            T component = comp.GetComponent<T>();
            if (component == null)
                Debug.LogError("Component of type " + typeof(T) + " not found", comp);
            return component;
        }
    }
}