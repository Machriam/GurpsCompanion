using System.Threading.Tasks;

namespace GurpsCompanion.Shared.Core
{
    public interface IFighterWeightNotificationClient
    {
        public const string HubConnection = "/FighterWeightHub";

        public Task ReceiveFighterWeigthChanged(long characterId, double weight);
    }
}
