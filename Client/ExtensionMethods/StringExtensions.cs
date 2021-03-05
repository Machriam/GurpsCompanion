namespace GurpsCompanion.Client.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string TruncateString(this string text, int count)
        {
            if (text.Length <= count) return text;
            return text.Substring(0, count) + "...";
        }
    }
}
