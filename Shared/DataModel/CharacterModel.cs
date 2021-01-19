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

        [DisplayAttribute]
        public long IsPlayer { get; set; }

        [DisplayAttribute]
        public string Name { get; set; }

        [DisplayAttribute]
        public long Strength { get; set; }

        [DisplayAttribute]
        public long Dexterity { get; set; }

        [DisplayAttribute]
        public long Intelligence { get; set; }

        [DisplayAttribute]
        public long Health { get; set; }

        [DisplayAttribute]
        public long WillMod { get; set; }

        [DisplayAttribute]
        public long PerceptionMod { get; set; }

        [DisplayAttribute]
        public long HitPointsMod { get; set; }

        [DisplayAttribute]
        public long BasicSpeedMod { get; set; }

        [DisplayAttribute]
        public long BasicMoveMod { get; set; }

        [DisplayAttribute]
        public long RadexFavor { get; set; }

        [DisplayAttribute]
        public long VagrexFavor { get; set; }

        [JsonIgnore]
        [DisplayAttribute]
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
