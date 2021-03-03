using System;

namespace GurpsCompanion.Shared.Features.PlayerView
{
    public class EncumbranceModel
    {
        public EncumbranceModel(double bM, double dodge, string description)
        {
            Description = description;
            BM = bM;
            Dodge = dodge;
        }

        public string Description { get; }
        public double BM { get; }
        public double Dodge { get; }
    }

    public interface IEncumbranceCalculator
    {
        EncumbranceModel GetEncumbrance(double basicLift, double weight, double bm, double dodge);
    }

    public class EncumbranceCalculator : IEncumbranceCalculator
    {
        public EncumbranceModel GetEncumbrance(double basicLift, double weight, double bm, double dodge)
        {
            if (weight <= basicLift) return new EncumbranceModel(bm, dodge, "No Encumbrance (Level 0)");
            if (weight <= basicLift * 2) return new EncumbranceModel(GetBM(bm, 0.8), dodge - 1, "Light Encumbrance (Level 1)");
            if (weight <= basicLift * 3) return new EncumbranceModel(GetBM(bm, 0.6), dodge - 2, "Medium Encumbrance (Level 2)");
            if (weight <= basicLift * 6) return new EncumbranceModel(GetBM(bm, 0.4), dodge - 3, "Heavy Encumbrance (Level 3)");
            if (weight <= basicLift * 10) return new EncumbranceModel(GetBM(bm, 0.2), dodge - 4, "Extra-Heavy Encumbrance (Level 4)");
            return new EncumbranceModel(0, 0, "Unable to move");
        }

        private static double GetBM(double bm, double encumbrance)
        {
            return Math.Max(1, (int)Math.Floor(bm * encumbrance));
        }
    }
}
