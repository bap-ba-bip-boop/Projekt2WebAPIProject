using AdministartionWebsite.Models;
using AdministartionWebsite.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;
using System.Diagnostics;

namespace AdministartionWebsite.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var hViewModel = new HomeIndexViewModel{
            CustomerAmount = _context.Customers!.Count(),
            NewestCustomerName = _context.Customers!.ToList().Last().CustomerName,
            ProjectAmount = _context.Projects!.Count(),
            MostProjectsForCustomer = _context.Customers!.Include(cust => cust.Projects).ToList().MaxBy(cust => cust.Projects!.Count())!.Projects!.Count(),
            AmountOfRegsToday = _context.TidsRegistrerings!.Where(reg => reg.Datum.Date == DateTime.Today).Count(),
            MostRegsProject = _context.Projects!.Include(proj => proj.TimeRegs).ToList().MaxBy(proj => proj.TimeRegs!.Count())!.ProjectName
        };
        return View(hViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}