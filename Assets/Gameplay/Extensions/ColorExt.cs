using UnityEngine;

namespace SkyClericExt
{
    public static class ColorExt
    {
        public static Color GetRandomColor
        {
            get
            {
                float r = Random.Range(0f, 1f);
                float g = Random.Range(0f, 1f);
                float b = Random.Range(0f, 1f);
                return new Color(r, g, b, 1);
            }
        }

        public static Color GetColorTransparent()
        {
            return new Color(1f, 1f, 1f, 0f);
        }
    }
}