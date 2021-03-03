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
        public void InvokeItemSelected(long itemId, long assId)
        {
            OnItemSelected?.Invoke(itemId, assId);
        }


        public event Action<long, long> OnItemSelected;
        public event Action OnItemChanged;

        public event Action OnSkillChanged;

        public event Action OnAdvantageChanged;

        public event Action OnGlossaryChanged;
    }
}
