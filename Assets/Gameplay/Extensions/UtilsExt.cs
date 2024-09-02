using System.Linq;

namespace SkyClericExt
{
    public static class UtilsExt
    {
        /// <summary>
        /// Проверяет, все ли элементы != null
        /// </summary>
        public static bool IsNullAny<T>(params T[] array)
        {
            if (array == null)
                return true;

            return array.Any(value => value == null);
        }

        /// <summary>
        /// Проверяет, все ли элементы string IsNullOrEmpty
        /// </summary>
        public static bool IsNullOrEmptyAny(params string[] array)
        {
            foreach (var item in array)
            {
                if (!string.IsNullOrEmpty(item))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Проверяет, все ли элементы string имеют значение
        /// </summary>
        public static bool IsNotNullOrEmptyAny(params string[] array)
        {
            foreach (var item in array)
            {
                if (string.IsNullOrEmpty(item))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Замена присваивания через временное поле
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}