using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System;

namespace Signal1.AspNetCore.SignalR{
    public class MessageHub :Hub{
        public  Task SendMessageToAll(string message){
           return   Clients.All.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToCaller(string message){
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToUser(string connectionId, string message){
            return Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync(){
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();

        }


        public override async Task OnDisconnectedAsync(Exception ex){
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }


    }
}