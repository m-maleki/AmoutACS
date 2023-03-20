namespace Framework.Core.Utilities
{
    public static class StringExtensions
    {
        public static string? ToCorrectDateTimeString(this string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            var day = str.Substring(0, 2);
            var month = str.Substring(3, 2);
            var year = str.Substring(6, 4);
            var time = str.Substring(11, 8);
            return $"{month}-{day}-{year} {time}";
        }
    }
}
