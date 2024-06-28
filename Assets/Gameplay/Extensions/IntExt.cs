namespace SkyClerikExt
{
    public static class IntExt
    {
        public static int GetPercen(this int value, byte percentage)
        {
            return (value * percentage) / 100;
        }
    }
}