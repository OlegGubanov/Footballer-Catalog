using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Footballer_Catalog.Models;

namespace Footballer_Catalog.Controllers;

public class HomeController : Controller
{
    private FootballerContext db;
    private IHubContext<FootballersHub> footballersContext;

    public HomeController(FootballerContext context, IHubContext<FootballersHub> footballersContext)
    {
        this.footballersContext = footballersContext;
        db = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await db.Footballers.ToListAsync());
    }
    
    public IActionResult Add()
    {
        ViewBag.Teams = db.Footballers.Select(footballer => footballer.Team).ToHashSet();
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(Footballer footballer)
    {
        db.Footballers.Add(footballer);
        await db.SaveChangesAsync();
        await footballersContext.Clients.All.SendAsync("Add", footballer);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id != null)
        {
            ViewBag.Teams = db.Footballers.Select(footballer => footballer.Team).ToHashSet();
            Footballer footballer = await db.Footballers.FirstOrDefaultAsync(footballer => footballer.Id == id);
            if (footballer != null)
                return View(footballer);
        }
        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Footballer footballer)
    {
        db.Footballers.Update(footballer);
        await db.SaveChangesAsync();
        await footballersContext.Clients.All.SendAsync("Edit", footballer);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            Footballer footballer = await db.Footballers.FirstOrDefaultAsync(footballer => footballer.Id == id);
            if (footballer != null)
            {
                db.Footballers.Remove(footballer);
                await db.SaveChangesAsync();
                await footballersContext.Clients.All.SendAsync("Delete", footballer.Id);
                return RedirectToAction("Index");
            }
        }
        return NotFound();
    }
}