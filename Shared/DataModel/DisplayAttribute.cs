using System;

namespace GurpsCompanion.Shared.DataModel
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class DisplayAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class EditableAttribute : Attribute
    {
    }
}
