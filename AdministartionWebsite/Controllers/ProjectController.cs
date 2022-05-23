using AdministartionWebsite.ViewModels.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;

namespace AdministartionWebsite.Controllers;

public class ProjectController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult ProjectIndex()
    {
        var piViewModel = new ProjectIndexViewModel
        {
            Projects = _context.Projects!.Include(proj => proj.Customer).Select(project =>
               new ProjectIndexVMListItem
               {
                   ProjectName = project.ProjectName,
                   Id = project.CustomerId,
                   CustomerName = project.Customer!.CustomerName
               }
            ).ToList()
        };
        return View(piViewModel);
    }
}
