using System.Linq;

namespace SkyClerikExt
{
    public static class StringExt
    {
        /// <summary>
        /// Возвращает значение, указывающее, содержит ли данная строка какую-нибудь из переданных подстрок.
        /// </summary>
        public static bool ContainsAny(this string str, params string[] values)
        {
            return values.Any(s => str.Contains(s));
        }

        /// <summary>
        /// Делит число запятыми стаками по три
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPriceStyle(int value)
        {
            string result = value.ToString();
            for (int count = result.Length, i = 3, t = 3; count >= i; i += 3, t += 4)
                result = result.Insert(result.Length - t, " ");

            return result;
        }

        public static string ToPriceStyle(this string str)
        {
            for (int count = str.Length, i = 3, t = 3; count >= i; i += 3, t += 4)
                str = str.Insert(str.Length - t, " ");

            return str;
        }
    }
}