using GurpsCompanion.Shared.Core;

namespace GurpsCompanion.Shared.Features.PlayerView
{
    public interface ISkillCpCalculator
    {
        long GetCpForSkill(long value, SkillDifficulties difficulty);
    }

    public class SkillCpCalculator : ISkillCpCalculator
    {
        public long GetCpForSkill(long value, SkillDifficulties difficulty)
        {
            switch (difficulty)
            {
                case SkillDifficulties.VH:
                    if (value == -3) return 1;
                    if (value == -2) return 2;
                    return (value + 2) * 4;

                case SkillDifficulties.H:
                    if (value == -2) return 1;
                    if (value == -1) return 2;
                    return (value + 1) * 4;

                case SkillDifficulties.A:
                    if (value == -1) return 1;
                    if (value == 0) return 2;
                    return value * 4;

                case SkillDifficulties.E:
                    if (value == 0) return 1;
                    if (value == 1) return 2;
                    return (value - 1) * 4;
            }
            return 0;
        }
    }
}
