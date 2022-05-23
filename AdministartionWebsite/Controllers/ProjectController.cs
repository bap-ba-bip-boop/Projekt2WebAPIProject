using AdministartionWebsite.ViewModels.Project;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;

namespace AdministartionWebsite.Controllers;

public class ProjectController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProjectController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IActionResult ProjectIndex()
    {
        var piViewModel = new ProjectIndexViewModel
        {
            Projects = _context.Projects!.Include(proj => proj.Customer)
                .Select(_mapper.Map<ProjectIndexVMListItem>)
                .ToList()
        };
        return View(piViewModel);
    }
    public IActionResult ProjectPage(int Id)
    {
        var Project = _context.Projects!
            .Include(proj => proj.TimeRegs)
            .First(proj => proj.ProjectId == Id);

        var ppViewModel = _mapper.Map<ProjectPageViewModel>(Project);
        return View(ppViewModel);
    }
}
