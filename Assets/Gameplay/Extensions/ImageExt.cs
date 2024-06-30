using UnityEngine;

namespace SkyClerikExt
{
    public static partial class ImageExt
    {
        public static Texture2D ToTexture(this Sprite sprite)
        {
            // Галочку read write в настройках!!!
            Rect tRect = sprite.textureRect;
            Texture2D icon = new Texture2D((int)tRect.width, (int)tRect.height);
            Color[] newTexture = sprite.texture.GetPixels((int)tRect.x, (int)tRect.y, (int)tRect.width, (int)tRect.height);
            icon.SetPixels(newTexture);
            icon.Apply();
            return icon;
        }
    }
}
