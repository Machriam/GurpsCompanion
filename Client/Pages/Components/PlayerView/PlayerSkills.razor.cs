using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Client.JsInterop;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public partial class PlayerSkills : ComponentBase, IDisposable
    {
        private IJsFunctionCallerService _jsService;

        protected override void OnInitialized()
        {
            _jsService = JsServiceFactory.Create(JavascriptGrids.NA, this);
            base.OnInitialized();
        }

        private CharacterModel _selectedCharacterModel;

        [Parameter]
        public CharacterModel SelectedCharacterModel
        {
            get => _selectedCharacterModel; set
            {
                if (_selectedCharacterModel == value) return;
                _selectedCharacterModel = value;
                OriginalCharacterModel = value?.Clone();
            }
        }

        public void GetPlayerSkills()
        {
        }

        [Parameter]
        public IEnumerable<SkillModel> Skills { get; set; }

        public CharacterModel OriginalCharacterModel { get; set; }

        public void UpdateStats()
        {
        }

        public void Dispose()
        {
            _jsService?.Dispose();
        }
    }
}
