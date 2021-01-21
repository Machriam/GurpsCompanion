using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GurpsCompanion.Shared.DataModel
{
    public abstract class ModelBase
    {
        public IEnumerable<string> GetPropertyNames(Type type)
        {
            return GetType().GetProperties().Where(p => TypeAttributeIsSet(p, type))
                .Select(p => Humanizer.StringHumanizeExtensions.Humanize(p.Name));
        }

        private static bool TypeAttributeIsSet(PropertyInfo propertyInfo, Type type)
        {
            return propertyInfo.CustomAttributes.Any(ca => ca.AttributeType == type);
        }

        public IEnumerable<object> GetPropertyValues(Type type)
        {
            return GetType().GetProperties().Where(p => TypeAttributeIsSet(p, type))
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
