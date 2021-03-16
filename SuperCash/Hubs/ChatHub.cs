using Microsoft.AspNetCore.SignalR;
using SuperCash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace SuperCash.Hubs
{
    public class ChatHub: Hub
    {    
        public override Task OnConnectedAsync()
        {
            ConnectedUsers.Connectados.Add(Context.ConnectionId);
            Console.WriteLine("Conectado", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {           
            ConnectedUsers.Connectados.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }


    }
}
