using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared.Core;

namespace GurpsCompanion.Client.Pages.Components
{
    public partial class DataList : ComponentBase
    {
        private readonly string _guid = Guid.NewGuid().ToString("N");

        [Parameter]
        public string LabelText { get; set; }

        [Parameter]
        public IEnumerable<IDataListItem> Items { get; set; }

        [Parameter]
        public EventCallback<IDataListItem> SelectedItemChanged { get; set; }

        private string _selectedText;

        public string SelectedText
        {
            get => _selectedText; set
            {
                if (value == _selectedText) return;
                _selectedText = value;
                var selectedItem = Items.FirstOrDefault(i => i.GetText() == value);
                SelectedItemChanged.InvokeAsync(selectedItem);
            }
        }
    }
}
