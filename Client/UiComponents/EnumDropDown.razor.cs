using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace GurpsCompanion.Client.UiComponents
{
    public partial class EnumDropDown<T> : ComponentBase where T : Enum
    {
        private T _selectedItem;

        protected override void OnInitialized()
        {
            Items = Enum.GetValues(typeof(T)).Cast<T>();
            base.OnInitialized();
        }

        [Parameter]
        public string Label { get; set; }

        public IEnumerable<T> Items { get; set; }

        [Parameter]
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem.Equals(value)) return;
                _selectedItem = value;
                SelectedItemChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<T> SelectedItemChanged { get; set; }
    }
}
