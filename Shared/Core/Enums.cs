using System.ComponentModel;

namespace GurpsCompanion.Shared.Core
{
    public enum BaseAttributes
    {
        [Description("HT")]
        HT,

        [Description("DX")]
        DX,

        [Description("IQ")]
        IQ,

        [Description("ST")]
        ST
    }

    public enum SkillDifficulties
    {
        [Description("Very Hard")]
        VH = 0,

        [Description("Hard")]
        H = 1,

        [Description("Average")]
        A = 2,

        [Description("Easy")]
        E = 3
    }
}
