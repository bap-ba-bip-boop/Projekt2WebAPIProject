using AdministartionWebsite.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;

namespace AdministartionWebsite.Controllers;

public class CustomerController : Controller
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult CustomerIndex()
    {
        var ciViewModel = new CustomerIndexViewModel
        {
            Customers = _context.Customers!.Select(customer =>
               new CustomerIndexVMListItem
               {
                   Name = customer.Name,
                   Id = customer.CustomerId
               }
            ).ToList()
        };
        return View(ciViewModel);
    }

    public IActionResult CustomerPage(int Id)
    {
        var customer = _context.Customers!.First(cust => cust.CustomerId == Id);

        var cpViewModel = new CustomerPageViewModel
        {
            Id = customer.CustomerId,
            CustomerName = customer.Name,
            CustomerProjects = _context.Projects!
                .Where(proj => proj.CustomerId == customer.CustomerId)
                .Include(proj => proj.TimeRegs)
                .Select(proj => new CustomerPageListItem
                {
                    Id = proj.ProjectId,
                    Name = proj.ProjectName,
                    regAmount = proj.TimeRegs!.Count(),
                    latestRegDate = proj.TimeRegs!.First().Datum
                }).ToList()
        };

        return View(cpViewModel);
    }
    [HttpGet]
    public IActionResult CustomerNew()
    {
        return View();
    }
    [HttpPost]
    public IActionResult CustomerNew(CustomerNewViewModel cnViewModel)
    {
        return View(cnViewModel);
    }
}
