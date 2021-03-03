using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using GurpsCompanion.Shared;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Pages
{
    public partial class FightView : ComponentBase
    {
        public double ElapsedFightingTime { get; set; }
        public CharacterModel CurrentlyFightingFighter { get; set; }
        public List<CharacterModel> Characters { get; set; }
        public List<CharacterModel> Fighters { get; set; } = new List<CharacterModel>();
        public CharacterModel SelectedCharacterForFight { get; set; }
        private string _selectedCharacterModelName;

        public string SelectedCharacterModelName
        {
            get => _selectedCharacterModelName; set
            {
                if (value == _selectedCharacterModelName) return;
                _selectedCharacterModelName = value;
                SelectedCharacterForFight = Characters.Find(c => c.Name == value);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Characters = await Http.GetFromJsonAsync<List<CharacterModel>>(string.Format(ApiAddressResources.Character_Base,
                StateContainer.PasswordModel.Hash, StateContainer.PasswordModel.Salt)).ConfigureAwait(false);
        }

        public void StartFight()
        {
            ElapsedFightingTime = 0d;
            NextStep();
        }

        public void NextStep()
        {
            if (CurrentlyFightingFighter != null) CurrentlyFightingFighter.NextFightingTime += CurrentlyFightingFighter.BasicSpeed;
            Fighters = Fighters.OrderBy(f => f.NextFightingTime).ThenBy(f => f == CurrentlyFightingFighter).ToList();
            CurrentlyFightingFighter = Fighters[0];
            ElapsedFightingTime = CurrentlyFightingFighter.NextFightingTime;
            CurrentlyFightingFighter.FightActionCounter++;
        }

        public void ResetFight()
        {
            ElapsedFightingTime = 0;
            Fighters = new List<CharacterModel>();
        }

        public void AddFighter()
        {
            var newFighter = SelectedCharacterForFight.Clone();
            var fightersOfSameType = Fighters.Where(n => n.Id == newFighter.Id);
            newFighter.FightingName = newFighter.Name;
            newFighter.NextFightingTime = newFighter.BasicSpeed + ElapsedFightingTime;
            if (fightersOfSameType.Any())
            {
                if (fightersOfSameType.Count() == 1) fightersOfSameType.First().FightingName += " 1";
                newFighter.FightingName +=
                    $" {fightersOfSameType.Max(f => int.Parse(f.FightingName.Remove(0, f.Name.Length + 1))) + 1}";
            }
            Fighters.Add(newFighter);
            Fighters = Fighters.OrderBy(f => f.NextFightingTime).ToList();
        }

        public void RemoveFighter(CharacterModel fighter)
        {
            Fighters.Remove(fighter);
            if (Fighters.Count == 0) ResetFight();
        }
    }
}
