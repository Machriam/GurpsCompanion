using System;

namespace GurpsCompanion.Shared.DataModel
{
    public interface INormalizeAttribute
    {
        decimal GetValue();
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class DisplayAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class EditableAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class CPAttribute : Attribute, INormalizeAttribute
    {
        public CPAttribute(int cp)
        {
            CP = cp;
        }

        public int CP { get; set; }

        public decimal GetValue()
        {
            return CP;
        }
    }
}
