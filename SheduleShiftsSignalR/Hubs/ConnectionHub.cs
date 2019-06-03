using Microsoft.AspNetCore.SignalR;
using SheduleShiftsSignalR.DataRunTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleShiftsSignalR.Hubs
{
    public class ConnectionHub : Hub
    {
        private readonly ConnectedList connectedList;

        public ConnectionHub()
        {
            connectedList = ConnectedList.GetInstance();
        }

        public async Task UserConnected(string userName)
        {
            connectedList.AddToDictionary(Context.ConnectionId, userName);

            await Clients.Others.SendAsync("OtherConnected", userName);
            await Clients.Client(Context.ConnectionId).SendAsync("Connected", Context.ConnectionId);
        }

        public async Task UserDisconected(string connectionId)
        {
            await Clients.Others.SendAsync("UserLogout", connectedList.GetUserName(connectionId));

            connectedList.RemoveFromDictionary(connectionId);
        }
    }
}
