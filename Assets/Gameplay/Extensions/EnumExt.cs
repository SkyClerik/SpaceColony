using System;

namespace SkyClericExt
{
    public static class EnumExt
    {
        private static Random Random = new Random();

        public static T GetRandom<T>() where T : Enum
        {
            Array array = Enum.GetValues(typeof(T));
            return (T)array.GetValue(Random.Next(array.Length));
        }
    }
}