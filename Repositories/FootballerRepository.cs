using Footballer_Catalog.Db;
using Footballer_Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Footballer_Catalog.Repositories;

public class FootballerRepository : IRepository<Footballer>
{
    private FootballerContext db;

    public FootballerRepository(FootballerContext db)
    {
        this.db = db;
    }

    public async Task<bool> Exists(int id)
    {
        return await db.Footballers.AsNoTracking().FirstOrDefaultAsync(footballer => footballer.Id == id) == null;
    }
    
    public async Task<List<Footballer>> GetAll()
    {
        return await db.Footballers.Include(footballer => footballer.Team).ToListAsync();
    }

    public async Task<Footballer> Get(int id)
    {
        return await db.Footballers.FirstOrDefaultAsync(footballer => footballer.Id == id);
    }

    public async Task Add(Footballer item)
    {
        await db.Footballers.AddAsync(item);
        await SaveChanges();
    }

    public async Task Edit(Footballer item)
    {
        db.Footballers.Update(item);
        await SaveChanges();
    }

    public async Task Delete(int id)
    {
        var footballer = await db.Footballers.FirstOrDefaultAsync(footballer => footballer.Id == id);
        if (footballer != null)
        {
            db.Footballers.Remove(footballer);
            await SaveChanges();
        }
    }

    public async Task SaveChanges()
    {
        await db.SaveChangesAsync();
    }
}