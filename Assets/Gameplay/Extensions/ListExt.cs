using System.Collections.Generic;

namespace SkyClericExt
{
    public static class List
    {
        public static void Add<T>(this List<T> list, params T[] values)
        {
            list.AddRange(values);
        }

        public static void Swap<T>(this List<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}