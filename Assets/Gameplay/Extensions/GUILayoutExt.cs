using UnityEngine;

namespace SkyClericExt
{
    public static class GUILayoutExt
    {
        public static bool BoxButton(string text, float size)
        {
            if (GUILayout.Button(text, GUILayout.Width(size), GUILayout.Height(size)))
            {
                return true;
            }
            return false;
        }

        public static bool BoxButton(Texture texture, float size)
        {
            if (GUILayout.Button(texture, GUILayout.Width(size), GUILayout.Height(size)))
            {
                return true;
            }
            return false;
        }
    }
}