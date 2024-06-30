using System.Collections.Generic;

namespace SkyClerikExt
{
    public static class DictionaryExtensions
    {
        public static TKey[] Shuffle<TKey, TValue>(
           this Dictionary<TKey, TValue> source)
        {
            System.Random r = new System.Random();
            TKey[] wviTKey = new TKey[source.Count];
            source.Keys.CopyTo(wviTKey, 0);

            for (int i = wviTKey.Length; i > 1; i--)
            {
                int k = r.Next(i);
                TKey temp = wviTKey[k];
                wviTKey[k] = wviTKey[i - 1];
                wviTKey[i - 1] = temp;
            }

            return wviTKey;
        }
    }
}