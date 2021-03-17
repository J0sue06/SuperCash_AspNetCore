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
            ConnectedUsers.Connectado = Context.ConnectionId;
            Console.WriteLine("Usuario conectado ID " + ConnectedUsers.Connectado);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Usuario Id " + Context.ConnectionId + " desconectado");
            ConnectedUsers.Connectado = string.Empty;      
            return base.OnDisconnectedAsync(exception);
        }


    }
}
