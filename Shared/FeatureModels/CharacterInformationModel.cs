using System.Collections.Generic;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Shared.FeatureModels
{
    public class CharacterInformationModel
    {
        public CharacterModel CharacterModel { get; set; }
        public IEnumerable<ItemModel> Items { get; set; }
        public IEnumerable<AdvantageModel> Advantages { get; set; }
        public IEnumerable<SkillModel> Skills { get; set; }
    }
}
