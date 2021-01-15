using System.Collections.Generic;
using System.Linq;

namespace GurpsCompanion.Shared.DataModel
{
    public class ModelBase
    {
        public IEnumerable<string> GetProperties()
        {
            return GetType().GetProperties().Select(p => Humanizer.StringHumanizeExtensions.Humanize(p.Name));
        }

        public IEnumerable<object> GetValues()
        {
            return GetType().GetProperties().Select(p => p.GetValue(this));
        }
    }
}
