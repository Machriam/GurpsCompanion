using System.Globalization;

namespace GurpsCompanion.Shared.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToCurrencyString(this decimal? value)
        {
            return value == null ? 0m.ToString("C", new CultureInfo("fr-Fr")) : value.Value.ToCurrencyString();
        }

        public static string ToCurrencyString(this decimal value)
        {
            return value.ToString("C", new CultureInfo("fr-Fr"));
        }

        public static string RoundTo(this decimal value, int position) => value.ToString("F" + position, new CultureInfo("fr-Fr"));
    }
}
