using System.Collections.Generic;
using UnityEngine;

namespace SkyClerikExt
{
    public static class GameObjectExt
    {
        public static void SetLayerRecursively(this GameObject inst, int layer)
        {
            inst.layer = layer;
            foreach (Transform child in inst.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }

        /// <summary>
        /// Gets a component from a game object (supports interfaces)
        /// </summary>
        public static T GetComponent<T>(this GameObject inst) where T : class
        {
            return inst.GetComponent(typeof(T)) as T;
        }

        public static GameObject FindTypeAboveObject<T>(this GameObject inst) where T : class
        {
            if (inst == null)
            {
                return null;
            }

            return FindTypeAboveObjectRecursive<T>(inst);
        }

        public static GameObject FindTypeAboveObjectRecursive<T>(this GameObject inst) where T : class
        {
            if (inst == null)
            {
                return null;
            }

            if (inst != null)
            {
                if (inst.GetComponent<T>() != null)
                {
                    return inst;
                }

                if (inst.transform.parent != null)
                {
                    return FindTypeAboveObjectRecursive<T>(inst.transform.parent.gameObject);
                }
            }

            return null;
        }

        public static Transform FindBelow(this GameObject inst, string name)
        {
            if (inst == null)
                return null;

            if (inst.transform.childCount == 0)
                return null;

            var child = inst.transform.Find(name);
            if (child != null)
            {
                return child;
            }

            foreach (GameObject t in inst.transform)
            {
                child = FindBelow(t, name);
                if (child != null)
                {
                    return child;
                }
            }

            return null;
        }

        public static void Show(this GameObject inst)
        {
            if (inst != null)
            {
                inst.SetActive(true);
            }
        }

        public static void Hide(this GameObject inst)
        {
            if (inst != null)
            {
                inst.SetActive(false);
            }
        }

        public static bool IsReady(this Object inst)
        {
            return inst != null ? true : false;
        }


        public static void StopSounds(this GameObject inst)
        {
            if (inst == null)
                return;

            GameObjectExt.StopSounds(inst);
        }

        public static void PauseSounds(this GameObject inst)
        {
            if (inst == null)
                return;

            GameObjectExt.PauseSounds(inst);
        }

        public static void PlaySounds(this GameObject inst)
        {
            if (inst == null)
                return;

            GameObjectExt.PlaySounds(inst);
        }

        public static bool IsRenderersVisible(this GameObject inst)
        {
            if (inst == null)
                return false;

            return GameObjectExt.IsRenderersVisible(inst);
        }

        public static bool IsAudioSourcePlaying(this GameObject inst)
        {
            if (inst == null)
                return false;

            return GameObjectExt.IsAudioSourcePlaying(inst);
        }

        public static void ShowRenderers(this GameObject inst)
        {
            if (inst == null)
                return;

            GameObjectExt.ShowRenderers(inst);
        }

        public static void HideRenderers(this GameObject inst)
        {
            if (inst == null)
                return;

            GameObjectExt.HideRenderers(inst);
        }

        public static void DestroyChildren(this GameObject inst)
        {
            if (inst == null)
                return;

            List<Transform> transforms = new List<Transform>();
            int b = 0;
            foreach (Transform t in inst.transform)
            {
                transforms.Add(t);
                b++;
            }

            foreach (Transform t in transforms)
            {
                t.parent = null;
                Object.Destroy(t.gameObject);
            }

            transforms.Clear();
        }

        public static void ChangeLayersRecursively(this GameObject inst, string name)
        {
            if (inst == null)
                return;

            foreach (Transform child in inst.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(name);
                ChangeLayersRecursively(child.gameObject, name);
            }
        }

    }
}