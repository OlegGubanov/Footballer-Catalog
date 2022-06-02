using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Footballer_Catalog.Models;
using Footballer_Catalog.Services;

namespace Footballer_Catalog.Controllers;

public class HomeController : Controller
{
    private IFootballersService footballersService;
    public HomeController(IFootballersService footballersService)
    {
        this.footballersService = footballersService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await footballersService.GetFootballers());
    }
}