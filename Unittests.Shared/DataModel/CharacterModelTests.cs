using System.Linq;
using FluentAssertions;
using GurpsCompanion.Shared.DataModel;
using Xunit;

namespace Unittests.Shared.DataModel
{
    public class CharacterModelTests
    {
        [Fact]
        public void CharacterModel_SumNormalizedAttributes_Test()
        {
            var sut = new CharacterModel()
            {
                Dexterity = 3,
                Strength = 2,
                Intelligence = 5,
                BasicMoveMod = 7,
                BasicSpeedMod = 11,
                HitPointsMod = 13,
                PerceptionMod = 17,
                WillMod = 19,
                Health = 23
            };
            var result = sut.GetNormalizedPropertyValues<CPAttribute>();
            result.Sum(v => v).Should().Be((2 * 10) + (3 * 20) + (5 * 20) + (7 * 5) + (11 * 5) + (13 * 2) + (17 * 5) + (19 * 5) + (23 * 8));
        }
    }
}
