using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace GurpsCompanion.Client.Pages.Components
{
    public interface DataListItem
    {
        string GetText();
    }

    public partial class DataList : ComponentBase
    {
        private readonly string _guid = Guid.NewGuid().ToString("N");

        [Parameter]
        public IEnumerable<DataListItem> Items { get; set; }

        [Parameter]
        public DataListItem SelectedItem { get; set; }

        [Parameter]
        public EventCallback<DataListItem> SelectedItemChanged { get; set; }

        private string _selectedText;

        public string SelectedText
        {
            get => _selectedText; set
            {
                if (value == _selectedText) return;
                _selectedText = value;
                SelectedItem = Items.First(i => i.GetText() == value);
                SelectedItemChanged.InvokeAsync(SelectedItem);
            }
        }
    }
}
