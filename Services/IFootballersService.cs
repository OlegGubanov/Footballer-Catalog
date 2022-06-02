using Footballer_Catalog.Models;

namespace Footballer_Catalog.Services;

public interface IFootballersService
{
    public Task Add(Footballer footballer);
    public Task<Footballer> GetFootballer(int id);
    public Task<List<Footballer>> GetFootballers();
    public Task<List<Team>> GetTeams();
    public Task Edit(Footballer footballer);
    public Task Delete(int id);
}