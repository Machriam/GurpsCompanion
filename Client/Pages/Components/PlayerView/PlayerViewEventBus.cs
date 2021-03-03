using System;
using System.Threading.Tasks;
using GurpsCompanion.Shared.DataModel;

namespace GurpsCompanion.Client.Shared
{
    public class PlayerViewEventBus
    {
        public void InvokeSelectedSkillChanged(SkillModel model) => OnSkillSelected?.Invoke(model);

        public void InvokeSelectedAdvantageChanged(AdvantageModel model) => OnAdvantageSelected?.Invoke(model);

        public void InvokeSelectedGlossaryChanged(GlossaryModel model) => OnGlossarySelected?.Invoke(model);

        public void InvokeSelectedItemChanged(ItemModel model) => OnItemSelected?.Invoke(model);

        public void InvokeSkillChanged() => OnSkillChanged?.Invoke();

        public void InvokeItemChanged() => OnItemChanged?.Invoke();

        public void InvokeAdvantageChanged() => OnAdvantageChanged?.Invoke();

        public void InvokeGlossaryChanged() => OnGlossaryChanged?.Invoke();

        public event Action<ItemModel> OnItemSelected;

        public event Action<SkillModel> OnSkillSelected;

        public event Action<AdvantageModel> OnAdvantageSelected;

        public event Action<GlossaryModel> OnGlossarySelected;

        public event Action OnItemChanged;

        public event Action OnSkillChanged;

        public event Action OnAdvantageChanged;

        public event Func<Task> OnGlossaryChanged;
    }
}
