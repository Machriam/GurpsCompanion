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

        public event Action OnItemChanged;

        public event Action OnSkillChanged;
    }
}
