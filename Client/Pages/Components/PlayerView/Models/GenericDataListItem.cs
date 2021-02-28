using GurpsCompanion.Shared.Core;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public class GenericDataListItem : IDataListItem
    {
        public string Name { get; }

        public GenericDataListItem(string name)
        {
            Name = name;
        }

        public string GetText => Name;
    }
}
