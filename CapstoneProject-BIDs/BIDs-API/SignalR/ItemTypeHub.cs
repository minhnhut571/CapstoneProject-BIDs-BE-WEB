using Data_Access.Entities;
using Microsoft.AspNetCore.SignalR;

namespace BIDs_API.SignalR
{
    public class ItemTypeHub:Hub
    {
        public async Task ItemTypeAdd(ItemType ItemType)
        {
            await Clients.All.SendAsync("ReceiveItemTypeAdd", ItemType);
        }

        public async Task ItemTypeUpdate(ItemType ItemType)
        {
            await Clients.All.SendAsync("ReceiveItemTypeUpdate", ItemType);
        }

        public async Task ItemTypeDelete(ItemType ItemType)
        {
            await Clients.All.SendAsync("ReceiveItemTypeDelete", ItemType);
        }
    }
}
