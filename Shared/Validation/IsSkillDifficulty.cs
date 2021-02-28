using System.Collections.Generic;

namespace GurpsCompanion.Shared.Validation
{
    public class IsSkillDifficulty
    {
        private readonly HashSet<string> _allowedDifficulties = new HashSet<string>() { 
            "VH",
            "H",

        };
    }
}
