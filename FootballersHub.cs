using Footballer_Catalog.Models;
using Microsoft.AspNetCore.SignalR;

namespace Footballer_Catalog;

public class FootballersHub : Hub
{
    public async Task Add(Footballer footballer)
    {
        await Clients.All.SendAsync("Add", footballer);
    }

    public async Task Edit(Footballer footballer)
    {
        await Clients.All.SendAsync("Edit", footballer);
    }
    
    public async Task Delete(int footballerId)
    {
        await Clients.All.SendAsync("Delete", footballerId);
    }
}