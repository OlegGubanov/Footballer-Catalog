using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Footballer_Catalog.Models;

public class FootballerContext : DbContext
{
    public DbSet<Footballer> Footballers { get; set; }

    public FootballerContext(DbContextOptions<FootballerContext> options) :
        base(options)
    {
        Database.EnsureCreated();
    }
}