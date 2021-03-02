using System;

namespace GurpsCompanion.Client.Shared
{
    public class PlayerViewEventBus
    {
        public void InvokeSkillChanged()
        {
            OnSkillChanged?.Invoke();
        }

        public void InvokeItemChanged()
        {
            OnItemChanged?.Invoke();
        }

        public void InvokeAdvantageChanged()
        {
            OnAdvantageChanged?.Invoke();
        }

        public void InvokeGlossaryChanged()
        {
            OnGlossaryChanged?.Invoke();
        }

        public event Action OnItemChanged;

        public event Action OnSkillChanged;

        public event Action OnAdvantageChanged;

        public event Action OnGlossaryChanged;
    }
}
