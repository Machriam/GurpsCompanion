using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages
{
    public partial class CharacterStats : ComponentBase
    {
        public List<CharacterModel> Characters { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Characters = await Http.GetFromJsonAsync<List<CharacterModel>>(ApiAddressResources.Character_Base).ConfigureAwait(false);
        }
    }
}
