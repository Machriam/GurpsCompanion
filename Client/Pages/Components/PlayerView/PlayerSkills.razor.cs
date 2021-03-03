using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Client.Core;
using GurpsCompanion.Client.JsInterop;
using GurpsCompanion.Client.UiComponents;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages.Components.PlayerView
{
    public partial class PlayerSkills : ComponentBase, IDisposable
    {
        private IJsFunctionCallerService _jsService;

        protected override void OnInitialized()
        {
            _jsService = JsServiceFactory.Create(JavascriptGrids.NA, this);
            SkillBaseAttributes = EnumConverter<SkillBaseAttributes>.GetDescriptions().Select(d => new GenericDataListItem(d));
            SkillDifficulties = EnumConverter<SkillDifficulties>.GetDescriptions().Select(d => new GenericDataListItem(d));
            EventBus.OnSkillSelected += SelectedSkillChanged;
            base.OnInitialized();
        }

        private CharacterModel _selectedCharacterModel;
        public CrudActions SubmitAction { get; set; }
        public IEnumerable<IDataListItem> SkillNames { get; set; }

        public IEnumerable<IDataListItem> SkillDifficulties { get; private set; }

        public IEnumerable<IDataListItem> SkillBaseAttributes { get; private set; }

        [Parameter]
        public CharacterModel SelectedCharacterModel
        {
            get => _selectedCharacterModel; set
            {
                if (_selectedCharacterModel == value) return;
                _selectedCharacterModel = value;
                GetAvailableSkills();
            }
        }

        public void SelectedSkillChanged(SkillModel model)
        {
            SkillEditModel = model;
            StateHasChanged();
        }

        public SkillModel SkillEditModel { get; set; } = new SkillModel();

        public async void SelectedSkillHasChanged(DataListEntry item)
        {
            if (item.SelectedItem != null)
            {
                SkillEditModel = await Http.GetFromJsonAsync<SkillModel>
                    (string.Format(ApiAddressResources.Skill_Get, item.SelectedItem.GetText)).ConfigureAwait(false);
            }
            else
            {
                SkillEditModel = new SkillModel() { Name = item.SelectedText };
            }
            StateHasChanged();
        }

        public async void GetAvailableSkills()
        {
            var skills = await Http.GetFromJsonAsync<List<string>>(ApiAddressResources.Skill_GetNames).ConfigureAwait(false);
            SkillNames = skills.Select(n => new GenericDataListItem(n));
            StateHasChanged();
        }

        public async void UpdateStats()
        {
            switch (SubmitAction)
            {
                case CrudActions.Delete:
                    using (var result = await Http.DeleteAsync(
                        string.Format(ApiAddressResources.Skill_Delete, SkillEditModel.Id, SelectedCharacterModel.Id)).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                    }
                    break;

                case CrudActions.Add:
                    using (var result = await Http.PostAsJsonAsync(
                               string.Format(ApiAddressResources.Skill_Post, SelectedCharacterModel.Id),
                               SkillEditModel
                                                                  ).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                        SkillEditModel = await result.Content.ReadFromJsonAsync<SkillModel>().ConfigureAwait(false);
                    }
                    break;

                case CrudActions.Update:
                    using (var result = await Http.PutAsJsonAsync(
                        string.Format(ApiAddressResources.Skill_Put, SelectedCharacterModel.Id),
                        SkillEditModel).ConfigureAwait(false))
                    {
                        if (!await _jsService.CheckHttpResponse(result).ConfigureAwait(false)) return;
                    }
                    break;
            }
            EventBus.InvokeSkillChanged();
        }

        public void Dispose()
        {
            _jsService?.Dispose();
            EventBus.OnSkillSelected -= SelectedSkillChanged;
        }
    }
}
