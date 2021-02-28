using System.Collections.Generic;
using FluentAssertions;
using GurpsCompanion.Shared.Core;
using GurpsCompanion.Shared.Extensions;
using Xunit;

namespace Unittests.Shared.Core
{
    public class EnumConverterTests
    {
        [Fact]
        public void GetDescriptions_StateUnderTest_ExpectedBehavior()
        {
            var expected = new List<string>()
            {
                SkillDifficulties.A.GetDescription(),
                SkillDifficulties.E.GetDescription(),
                SkillDifficulties.VH.GetDescription(),
                SkillDifficulties.H.GetDescription(),
            };
            var expectedBaseAttributes = new List<string>()
            {
                SkillBaseAttributes.DX.GetDescription(),
                SkillBaseAttributes.HT.GetDescription(),
                SkillBaseAttributes.IQ.GetDescription(),
                SkillBaseAttributes.ST.GetDescription(),
            };
            var result = EnumConverter<SkillDifficulties>.GetDescriptions();
            var result2 = EnumConverter<SkillDifficulties>.GetDescriptions();
            var result3 = EnumConverter<SkillBaseAttributes>.GetDescriptions();
            result.Should().BeEquivalentTo(expected);
            result2.Should().BeEquivalentTo(expected);
            result3.Should().BeEquivalentTo(expectedBaseAttributes);
        }

        [Fact]
        public void ConvertTo_StateUnderTest_ExpectedBehavior()
        {
            var result = EnumConverter<SkillDifficulties>.ConvertTo(SkillDifficulties.VH.GetDescription());
            result.Should().Be(SkillDifficulties.VH);
        }
    }
}
