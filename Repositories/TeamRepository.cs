using Footballer_Catalog.Models;
using Microsoft.EntityFrameworkCore;
using Footballer_Catalog.Db;

namespace Footballer_Catalog.Repositories;

public class TeamRepository : IRepository<Team>
{
    private FootballerContext db;

    public TeamRepository(FootballerContext db)
    {
        this.db = db;
    }

    public async Task<bool> Exists(int id)
    {
        return await db.Teams.AsNoTracking().FirstOrDefaultAsync(team => team.Id == id) == null;
    }
    
    public async Task<List<Team>> GetAll()
    {
        return await db.Teams.ToListAsync();
    }

    public async Task<Team> Get(int id)
    {
        return await db.Teams.FirstOrDefaultAsync(team => team.Id == id);
    }

    public async Task Add(Team item)
    {
        await db.Teams.AddAsync(item);
        await SaveChanges();
    }

    public async Task Edit(Team item)
    {
        db.Teams.Update(item);
        await SaveChanges();
    }
    

    public async Task Delete(int id)
    {
        var team = await db.Teams.FirstOrDefaultAsync(team => team.Id == id);
        if (team != null)
        {
            db.Teams.Remove(team);
            await SaveChanges();
        }
    }

    public async Task SaveChanges()
    {
        await db.SaveChangesAsync();
    }
}