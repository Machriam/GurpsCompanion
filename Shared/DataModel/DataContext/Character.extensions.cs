#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class Character
    {
        public Character()
        {
        }

        public Character(CharacterModel model)
        {
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
    }
}
