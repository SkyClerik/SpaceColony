using UnityEngine;
using System.Text;

namespace SkyClerikExt
{
    public static class GameObjectHelperExt
    {
        public static void StopSounds(GameObject inst)
        {
            if (inst == null)
                return;

            if (inst.GetComponent<AudioSource>() != null)
            {
                if (inst.GetComponent<AudioSource>().isPlaying)
                {
                    inst.GetComponent<AudioSource>().Stop();
                }
            }

            foreach (AudioSource source in inst.GetComponentsInChildren<AudioSource>())
            {
                source.Stop();
            }
        }

        public static void PauseSounds(GameObject inst)
        {
            if (inst == null)
                return;

            if (inst.GetComponent<AudioSource>() != null)
            {
                if (inst.GetComponent<AudioSource>().isPlaying)
                {
                    inst.GetComponent<AudioSource>().Pause();
                }
            }

            foreach (AudioSource source in inst.GetComponentsInChildren<AudioSource>())
            {
                source.Pause();
            }
        }

        public static void PlaySounds(GameObject inst)
        {
            if (inst == null)
                return;

            if (inst.GetComponent<AudioSource>() != null)
            {
                if (!inst.GetComponent<AudioSource>().isPlaying)
                {
                    inst.GetComponent<AudioSource>().Play();
                }
            }

            foreach (AudioSource source in inst.GetComponentsInChildren<AudioSource>())
            {
                source.Play();
            }
        }


        public static bool IsAudioSourcePlaying(GameObject inst)
        {
            if (inst == null)
                return false;

            if (inst.GetComponent<AudioSource>() != null)
            {
                if (inst.GetComponent<AudioSource>().isPlaying)
                {
                    return true;
                }
            }

            foreach (AudioSource source in inst.GetComponentsInChildren<AudioSource>())
            {
                if (source.isPlaying)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsRenderersVisible(GameObject inst)
        {
            if (inst == null)
                return false;

            if (inst.GetComponent<Renderer>() != null)
            {
                if (inst.GetComponent<Renderer>().enabled)
                {
                    return true;
                }
            }

            Renderer[] rendererComponents = inst.GetComponentsInChildren<Renderer>();

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                if (component.enabled)
                {
                    return true;
                }
            }

            return false;
        }

        public static void ShowRenderers(GameObject inst)
        {
            if (inst == null)
                return;

            if (inst.GetComponent<Renderer>() != null)
            {
                inst.GetComponent<Renderer>().enabled = true;
            }

            Renderer[] rendererComponents = inst.GetComponentsInChildren<Renderer>();

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }
        }

        public static void HideRenderers(GameObject inst)
        {
            if (inst == null)
                return;

            if (inst.GetComponent<Renderer>() != null)
            {
                inst.GetComponent<Renderer>().enabled = false;
            }

            Renderer[] rendererComponents = inst.GetComponentsInChildren<Renderer>();

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }
        }

        public static void DumpRootTransforms()
        {
            Object[] objs = Object.FindObjectsByType(typeof(GameObject), FindObjectsSortMode.None);
            foreach (Object obj in objs)
            {
                GameObject go = obj as GameObject;
                if (go.transform.parent == null)
                {
                    DumpGoToLog(go);
                }
            }
        }

        public static void DumpGoToLog(GameObject go)
        {
            Debug.Log($"DUMP: go: {go.name} :::: {DumpGo(go)}");
        }

        public static string DumpGo(GameObject go)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(go.name);
            DumpGameObject(go, sb, "", false);
            return sb.ToString();
        }

        private static void DumpGameObject(GameObject gameObject, StringBuilder sb, string indent, bool includeAllComponents)
        {
            bool rendererEnabled = false;
            if (gameObject.GetComponent<Renderer>() != null)
            {
                rendererEnabled = gameObject.GetComponent<Renderer>().enabled;
            }
            int markerId = -1;
            bool hasLoadedObj = false;

            sb.Append(string.Format($"\r\n{indent} - name: {gameObject.name}\n" +
                $" - a: {gameObject.activeSelf} - r: {rendererEnabled} - mid: {markerId} - loadedObj: {hasLoadedObj}\n" +
                $" - scale: x: {gameObject.transform.localScale.x} y: {gameObject.transform.localScale.y} z: {gameObject.transform.localScale.z}\n" +
                $" - pos: x: {gameObject.transform.position.x} y: {gameObject.transform.position.y} z: {gameObject.transform.position.z}\n"));

            if (includeAllComponents)
            {
                foreach (Component component in gameObject.GetComponents<Component>())
                {
                    DumpComponent(component, sb, indent + "  ");
                }
            }

            foreach (Transform child in gameObject.transform)
            {
                DumpGameObject(child.gameObject, sb, indent + "  ", includeAllComponents);
            }
        }

        private static void DumpComponent(Component component, StringBuilder sb, string indent)
        {
            sb.Append(string.Format("{0}{1}", indent, (component == null ? "(null)" : component.GetType().Name)));
        }
    }
}