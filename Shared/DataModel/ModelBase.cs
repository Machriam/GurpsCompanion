using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GurpsCompanion.Shared.DataModel
{
    public class ModelBase
    {
        public IEnumerable<string> GetDisplayPropertyNames()
        {
            return GetType().GetProperties().Where(DisplayAttributeIsSet)
                .Select(p => Humanizer.StringHumanizeExtensions.Humanize(p.Name));
        }

        private static bool DisplayAttributeIsSet(PropertyInfo propertyInfo)
        {
            return propertyInfo.CustomAttributes.Any(ca => ca.AttributeType == typeof(DisplayAttribute));
        }

        public IEnumerable<object> GetDisplayValues()
        {
            return GetType().GetProperties().Where(DisplayAttributeIsSet)
                .Select(p => FormatObject(p.GetValue(this)));
        }

        private static string FormatObject(object value) => value switch
        {
            double v => v.ToString("N2"),
            decimal v => v.ToString("N2"),
            float v => v.ToString("N2"),
            _ => value.ToString(),
        };
    }
}
