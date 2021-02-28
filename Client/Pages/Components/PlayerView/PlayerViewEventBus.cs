using System;

namespace GurpsCompanion.Client.Shared
{
    public class PlayerViewEventBus
    {
        public void InvokeItemChanged()
        {
            OnItemChanged?.Invoke();
        }

        public event Action OnItemChanged;
    }
}
