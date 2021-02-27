using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared.Core;

namespace GurpsCompanion.Client.UiComponents
{
    public class DataListEntry
    {
        public DataListEntry(IDataListItem selectedItem, string selectedText)
        {
            SelectedItem = selectedItem;
            SelectedText = selectedText;
        }

        public IDataListItem SelectedItem { get; }
        public string SelectedText { get; }
    }

    public partial class DataList : ComponentBase
    {
        private readonly string _guid = Guid.NewGuid().ToString("N");

        [Parameter]
        public Func<IDataListItem, bool> InputValidator { get; set; }

        [Parameter]
        public string LabelText { get; set; }

        private IEnumerable<IDataListItem> _items;

        [Parameter]
        public IEnumerable<IDataListItem> Items
        {
            get => _items; set
            {
                if (_items == value) return;
                if (InputValidator != null)
                {
                    _items = value.Where(InputValidator);
                }
                else
                {
                    _items = value;
                }
                if (_items.Count() == 1) InitialSelectedItem = _items.First();
                else InitialSelectedItem = null;
            }
        }

        [Parameter]
        public EventCallback<DataListEntry> InputHasChanged { get; set; }

        [Parameter]
        public EventCallback<IDataListItem> SelectedItemChanged { get; set; }

        private IDataListItem _initialSelectedItem;

        [Parameter]
        public IDataListItem InitialSelectedItem
        {
            get => _initialSelectedItem; set
            {
                if (value == _initialSelectedItem) return;
                _initialSelectedItem = value;
                SelectedText = _initialSelectedItem?.GetText;
                StateHasChanged();
            }
        }

        private string _selectedText;

        public string SelectedText
        {
            get => _selectedText; set
            {
                if (value == _selectedText) return;
                _selectedText = value;
                var selectedItem = Items?.FirstOrDefault(i => i.GetText == value);
                InputHasChanged.InvokeAsync(new DataListEntry(selectedItem, value));
                SelectedItemChanged.InvokeAsync(selectedItem);
                StateHasChanged();
            }
        }
    }
}
