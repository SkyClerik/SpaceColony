namespace SkyClericExt
{
    public static class IntExt
    {
        public static int GetPercent(this int value, byte percentage)
        {
            return (value * percentage) / 100;
        }
    }
}