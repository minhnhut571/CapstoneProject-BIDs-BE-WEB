using Data_Access.Entities;
using Microsoft.AspNetCore.SignalR;

namespace BIDs_API.SignalR
{
    public class RoleHub:Hub
    {
        public async Task RoleAdd(Role Role)
        {
            await Clients.All.SendAsync("ReceiveRoleAdd", Role);
        }

        public async Task RoleUpdate(Role Role)
        {
            await Clients.All.SendAsync("ReceiveRoleUpdate", Role);
        }

        public async Task RoleDelete(Role Role)
        {
            await Clients.All.SendAsync("ReceiveRoleDelete", Role);
        }
    }
}
