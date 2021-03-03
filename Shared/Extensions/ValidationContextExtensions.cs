using System.ComponentModel.DataAnnotations;

namespace GurpsCompanion.Shared.Extensions
{
    public static class ValidationContextExtensions
    {
        public static T GetValue<T>(this ValidationContext context, string attribute)
        {
            var instance = context.ObjectInstance;
            var type = instance.GetType();
            return (T)type.GetProperty(attribute).GetValue(instance);
        }
    }
}
