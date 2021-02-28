using System;
using System.Collections.Generic;
using System.Linq;
using GurpsCompanion.Shared.Extensions;

namespace GurpsCompanion.Shared.Core
{
    public static class EnumConverter<T> where T : Enum
    {
        private static readonly Dictionary<Type, Dictionary<string, T>> s_typeDictionary
            = new Dictionary<Type, Dictionary<string, T>>();

        public static IEnumerable<string> GetDescriptions()
        {
            InitializeDictionary();
            return s_typeDictionary[typeof(T)].Keys;
        }

        public static T ConvertTo(string value)
        {
            InitializeDictionary();
            return s_typeDictionary[typeof(T)][value];
        }

        private static void InitializeDictionary()
        {
            if (!s_typeDictionary.ContainsKey(typeof(T)))
            {
                var typeDictionary = Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(e => e.GetDescription());
                s_typeDictionary.Add(typeof(T), typeDictionary);
            }
        }
    }
}
