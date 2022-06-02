using System.Globalization;
using Footballer_Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Footballer_Catalog.Db;

public class FootballerContext : DbContext
{
    public DbSet<Footballer> Footballers { get; set; }
    public DbSet<Team> Teams { get; set; }

    public FootballerContext(DbContextOptions<FootballerContext> options) :
        base(options)
    {
        Database.EnsureCreated();
    }
}