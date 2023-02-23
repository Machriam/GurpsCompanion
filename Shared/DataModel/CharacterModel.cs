using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.DataModel.DataContext;

namespace GurpsCompanion.Shared.DataModel
{
    public class CharacterModel : ModelBase, IDataListItem
    {
        public CharacterModel Clone()
        {
            return (CharacterModel)MemberwiseClone();
        }

        public string GetText => Name;

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
        [CP(10)]
        [Range(6, 99)]
        public long Strength { get; set; }

        [Display]
        [Editable]
        [CP(20)]
        [Range(6, 99)]
        public long Dexterity { get; set; }

        [Display]
        [Editable]
        [CP(20)]
        [Range(6, 99)]
        public long Intelligence { get; set; }

        [Display]
        [Editable]
        [CP(8)]
        [Range(6, 99)]
        public long Health { get; set; }

        [Display]
        [Editable]
        [CP(5)]
        [Range(-10, 99)]
        public long WillMod { get; set; }

        [Display]
        [Editable]
        [CP(5)]
        [Range(-10, 99)]
        public long PerceptionMod { get; set; }

        [Display]
        [Editable]
        [CP(2)]
        [Range(-10, 99)]
        public long HitPointsMod { get; set; }

        [Display]
        [Editable]
        [CP(5)]
        [Range(-10, 99)]
        public long BasicSpeedMod { get; set; }

        [Display]
        [Editable]
        [CP(5)]
        [Range(-10, 99)]
        public long BasicMoveMod { get; set; }

        [Display]
        public long RadexFavor { get; set; }

        [Display]
        public long VagrexFavor { get; set; }

        [JsonIgnore]
        [Display]
        public double BasicSpeed => 1d / ((Dexterity + BasicSpeedMod + Health + FightBasicSpeedMod + FightDexterityMod + FightHealthMod) / 4d);

        [JsonIgnore]
        [Display]
        public double Dodge => (1d / BasicSpeed) + 3;

        [JsonIgnore]
        public double NextFightingTime { get; set; }

        [JsonIgnore]
        public double FightStrengthMod { get; set; }

        [JsonIgnore]
        public double FightBasicSpeedMod { get; set; }

        [JsonIgnore]
        public double FightDexterityMod { get; set; }

        [JsonIgnore]
        public double FightHealthMod { get; set; }

        [JsonIgnore]
        public double FightingWeight { get; set; }

        [JsonIgnore]
        [Display]
        public long Will => Intelligence + WillMod;

        [JsonIgnore]
        [Display]
        public long Perception => Intelligence + PerceptionMod;

        [JsonIgnore]
        [Display]
        public long HitPoints => Strength + HitPointsMod;

        [JsonIgnore]
        [Display]
        public double BasicMove => Math.Floor((1 / BasicSpeed) + (BasicMoveMod / 4));

        [JsonIgnore]
        [Display]
        public decimal StrenghtRegeneration => Math.Floor(Strength / 10m);

        [JsonIgnore]
        [Display]
        public decimal HitPointsRegeneration => Math.Floor(HitPoints / 10m);

        [JsonIgnore]
        [Display]
        public decimal VagrexPointRegeneration => VagrexFavor <= 0 ? 0 : Math.Ceiling(VagrexFavor * 10m / (10m + VagrexFavor));

        [JsonIgnore]
        [Display]
        public long VagrexPoints => Math.Max(0, (long)(Math.Round((Health * 0.5) + (Strength * 0.2), 0) + VagrexFavor));

        [JsonIgnore]
        [Display]
        public double BasicLift
        {
            get
            {
                var strength = Strength + FightStrengthMod;
                return strength > 7 ? Math.Round(strength * strength / 5d, 0) : strength * strength / 5;
            }
        }

        [JsonIgnore]
        [Display]
        public long NeededCP => GetNormalizedPropertyValues<CPAttribute>().Sum(v => (long)v);

        [JsonIgnore]
        public string FightingComment { get; set; }

        [JsonIgnore]
        public string FightingName { get; set; }

        [JsonIgnore]
        public int FightActionCounter { get; set; }
    }
}
