using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using Footballer_Catalog.Models;
using Footballer_Catalog.Services;
using Microsoft.EntityFrameworkCore;

namespace Footballer_Catalog.Controllers;

public class FootballersController : Controller
{
    private IFootballersService footballersService;
    
    public FootballersController(IFootballersService footballersService)
    {
        this.footballersService = footballersService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        ViewBag.Teams = (await footballersService.GetTeams()).Select(team => team.Name);
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(Footballer footballer)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Teams = (await footballersService.GetTeams()).Select(team => team.Name);
            return View(footballer);
        }
        await footballersService.Add(footballer);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id.HasValue)
        {
            ViewBag.Teams = (await footballersService.GetTeams()).Select(team => team.Name);
            var footballer = await footballersService.GetFootballer(id.Value);
            if (footballer != null)
                return View(footballer);
        }
        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Footballer footballer)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Teams = (await footballersService.GetTeams()).Select(team => team.Name);
            return View(footballer);
        }
        await footballersService.Edit(footballer);
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id.HasValue)
        {
            await footballersService.Delete(id.Value);
            return RedirectToAction("Index", "Home");
        }
        return NotFound();
    }
}