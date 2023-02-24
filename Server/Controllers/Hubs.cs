using GurpsCompanion.Shared.Core;
using Microsoft.AspNetCore.SignalR;

namespace GurpsCompanion.Server.Controllers
{
    public class FighterHub : Hub<IFighterWeightNotificationClient>
    {
    }
}
