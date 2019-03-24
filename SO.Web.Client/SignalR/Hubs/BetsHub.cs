using Microsoft.AspNetCore.SignalR;
using SO.Server.FeedConsumer.DTOs;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SO.Web.Client.SignalR.Hubs
{
    public class BetsHub : Hub
    {
        public async Task Subscribe(int matchId)
        {
        }
    }
}
