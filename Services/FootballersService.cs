using Footballer_Catalog.Models;
using Footballer_Catalog.Repositories;
using Microsoft.AspNetCore.SignalR;
using Footballer_Catalog.Hubs;

namespace Footballer_Catalog.Services;

public class FootballersService : IFootballersService
{
    private IRepository<Footballer> footballersRepository;
    private IRepository<Team> teamsRepository;
    private IHubContext<FootballersHub> footballersHub;

    public FootballersService(IRepository<Footballer> footballersRepository,
        IRepository<Team> teamsRepository,
        IHubContext<FootballersHub> footballersHub)
    {
        this.footballersRepository = footballersRepository;
        this.teamsRepository = teamsRepository;
        this.footballersHub = footballersHub;
    }

    public async Task Add(Footballer footballer)
    {
        await SetFootballerTeam(footballer);
        await footballersRepository.Add(footballer);
        await footballersHub.Clients.All.SendAsync("Add", footballer);
    }

    public async Task<Footballer> GetFootballer(int id)
    {
        return await footballersRepository.Get(id);
    }

    public async Task<List<Footballer>> GetFootballers()
    {
        return await footballersRepository.GetAll();
    }

    private async Task SetFootballerTeam(Footballer footballer)
    {
        var teams = await teamsRepository.GetAll();
        var team = teams.FirstOrDefault(team => team.Name == footballer.Team.Name);
        if (team == null)
            await teamsRepository.Add(footballer.Team);
        else
            footballer.Team = team;
    }

    public async Task<List<Team>> GetTeams()
    {
        return await teamsRepository.GetAll();
    }

    public async Task Edit(Footballer footballer)
    {
        if ((await footballersRepository.Exists(footballer.Id)))
            return;
        await SetFootballerTeam(footballer);
        await footballersRepository.Edit(footballer);
        await footballersHub.Clients.All.SendAsync("Edit", footballer);
    }

    public async Task Delete(int id)
    {
        await footballersRepository.Delete(id);
        await footballersHub.Clients.All.SendAsync("Delete", id);
    }
}
