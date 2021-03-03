using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared.Core;

namespace GurpsCompanion.Client.UiComponents
{
    public partial class TabNavigation<T> : ComponentBase where T : Enum
    {
        public Dictionary<T, string> Headers { get; set; }
        private T _selectedItem;

        [Parameter]
        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value.Equals(_selectedItem)) return;
                _selectedItem = value;
                SelectedItemChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<T> SelectedItemChanged { get; set; }

        protected override void OnInitialized()
        {
            Headers = EnumConverter<T>.GetDescriptions()
                .Select(d => (EnumConverter<T>.ConvertTo(d), d))
                .ToDictionary(d => d.Item1, d => d.d);
            SelectedItem = Headers.First().Key;
            base.OnInitialized();
        }
    }
}
