using GurpsCompanion.Shared.Core;

namespace GurpsCompanion.Shared.Features.PlayerView
{
    public interface ISkillCpCalculator
    {
        long GetSkillModifier(long value, SkillDifficulties difficulty);

        long GetCpForSkill(long value);
    }

    public class SkillCpCalculator : ISkillCpCalculator
    {
        public long GetCpForSkill(long value)
        {
            if (value == 0) return 0;
            if (value == 1) return 1;
            if (value == 2) return 2;
            return (value - 2) * 4;
        }

        public long GetSkillModifier(long value, SkillDifficulties difficulty)
        {
            switch (difficulty)
            {
                case SkillDifficulties.VH: return -4 + value;
                case SkillDifficulties.H: return -3 + value;
                case SkillDifficulties.A: return -2 + value;
                case SkillDifficulties.E: return -1 + value;
                default: break;
            }
            return 0;
        }
    }
}
