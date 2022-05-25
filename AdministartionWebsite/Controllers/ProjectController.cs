using AdministartionWebsite.ViewModels.Project;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;

namespace AdministartionWebsite.Controllers;

[Authorize]
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
            .Include(proj => proj.Customer)
            .Include(proj => proj.TimeRegs)
            .First(proj => proj.ProjectId == Id);

        var ppViewModel = _mapper.Map<ProjectPageViewModel>(Project);
        return View(ppViewModel);
    }
    [HttpGet]
    public IActionResult ProjectNew()
    {
        var pnViewModel = new ProjectNewViewModel{
            CustomerList = getCustomerSelectList()
        };
        return View(pnViewModel);
    }
    [HttpPost]
    public IActionResult ProjectNew(ProjectNewViewModel pnViewModel)
    {
        if(ModelState.IsValid)
        {
            var newProject = _mapper.Map<Project>(pnViewModel);
            _context.Projects!.Add(newProject);
            _context.SaveChanges();

            return RedirectToAction(nameof(ProjectIndex));
        }
        pnViewModel.CustomerList = getCustomerSelectList();
        return View(pnViewModel);
    }
    private List<SelectListItem> getCustomerSelectList()
    {
        var ListOfItems = _context.Customers!
            .Select(cust => 
                new SelectListItem{
                    Text=cust.CustomerName,
                    Value=$"{cust.CustomerId}"
                }
            ).ToList();
        ListOfItems.Insert(
            0,
            new SelectListItem{Text="...Select a Customer...", Value="0", Selected=true, Disabled=true}
            );
        return ListOfItems;
    }
    [HttpGet]
    public IActionResult ProjectEdit(int Id)
    {
        var ProjectToEdit = _context.Projects!.First(proj => proj.ProjectId == Id);
        var peViewModel = _mapper.Map<ProjectEditViewModel>(ProjectToEdit);
        return View(peViewModel);
    }
    [HttpPost]
    public IActionResult ProjectEdit(ProjectEditViewModel pedViewModel)
    {
        if(ModelState.IsValid)
        {
            var ProjectToEdit = _context.Projects!.First(proj => proj.ProjectId == pedViewModel.ProjectId);
            
            _mapper.Map(pedViewModel, ProjectToEdit);
            _context.SaveChanges();

            return RedirectToAction(nameof(ProjectIndex));
        }
        return View(pedViewModel);
    }
    public IActionResult ProjectDelete(int Id)
    {
        var projectToDelete = _context.Projects!.First(proj => proj.ProjectId == Id);

        _context.Projects!.Remove(projectToDelete);
        _context.SaveChanges();

        return RedirectToAction(nameof(ProjectIndex));
    }
}
