using System;
using System.Collections.Generic;
using System.Linq;
using GurpsCompanion.Shared.Extensions;

namespace GurpsCompanion.Shared.Core
{
    public static class EnumConverter<T> where T : Enum
    {
        private static Dictionary<string, T> s_typeDictionary
            = new Dictionary<string, T>();

        public static IEnumerable<string> GetDescriptions()
        {
            InitializeDictionary();
            return s_typeDictionary.Keys;
        }

        public static T ConvertTo(string value)
        {
            InitializeDictionary();
            return s_typeDictionary[value];
        }

        private static void InitializeDictionary()
        {
            if (s_typeDictionary.Count > 0) return;
            s_typeDictionary = Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(e => e.GetDescription());
        }
    }
}
