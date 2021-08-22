namespace System
{
    public static class StringsExtensions
    {
        public static int ToInt32(this string value)
        {
            return string.IsNullOrEmpty(value) ? 0 : Convert.ToInt32(value);
        }
    }
}