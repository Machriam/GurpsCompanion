using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GurpsCompanion.Shared.DataModel.DataContext;

namespace GurpsCompanion.Shared.DataModel
{
    public class CharacterModel : ModelBase
    {
        public CharacterModel Clone()
        {
            return (CharacterModel)MemberwiseClone();
        }

        public CharacterModel()
        {
        }

        public CharacterModel(Character model)
        {
            Id = model.Id;
            IsPlayer = model.IsPlayer;
            Name = model.Name;
            Strength = model.Strength;
            Dexterity = model.Dexterity;
            Intelligence = model.Intelligence;
            Health = model.Health;
            WillMod = model.WillMod;
            PerceptionMod = model.PerceptionMod;
            HitPointsMod = model.HitPointsMod;
            BasicSpeedMod = model.BasicSpeedMod;
            BasicMoveMod = model.BasicMoveMod;
            RadexFavor = model.RadexFavor;
            VagrexFavor = model.VagrexFavor;
        }

        public long Id { get; set; }

        [Display]
        public long IsPlayer { get; set; }

        [Display]
        public string Name { get; set; }

        [Display]
        [Editable]
        [Range(6, 99)]
        public long Strength { get; set; }

        [Display]
        [Editable]
        [Range(6, 99)]
        public long Dexterity { get; set; }

        [Display]
        [Editable]
        [Range(6, 99)]
        public long Intelligence { get; set; }

        [Display]
        [Editable]
        [Range(6, 99)]
        public long Health { get; set; }

        [Display]
        [Editable]
        [Range(-10, 99)]
        public long WillMod { get; set; }

        [Display]
        [Editable]
        [Range(-10, 99)]
        public long PerceptionMod { get; set; }

        [Display]
        [Editable]
        [Range(-10, 99)]
        public long HitPointsMod { get; set; }

        [Display]
        [Editable]
        [Range(-10, 99)]
        public long BasicSpeedMod { get; set; }

        [Display]
        [Editable]
        [Range(-10, 99)]
        public long BasicMoveMod { get; set; }

        [Display]
        public long RadexFavor { get; set; }

        [Display]
        public long VagrexFavor { get; set; }

        [JsonIgnore]
        [Display]
        public double BasicSpeed => 1d / ((Dexterity + BasicSpeedMod + Health) / 4d);

        [JsonIgnore]
        public double NextFightingTime { get; set; }

        [JsonIgnore]
        public string FightingComment { get; set; }

        [JsonIgnore]
        public string FightingName { get; set; }

        [JsonIgnore]
        public int FightActionCounter { get; set; }
    }
}
