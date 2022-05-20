using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;
using SharedResources.Services;
using WebAPI.DTO.Project;
using WebAPI.Model;
using WebAPI.Services;
using static SharedResources.Services.IDbLookupService;

namespace WebAPI.Controllers;

[ApiController]
[Route("project")]
public class ProjectController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly IDbLookupService _lookup;
    private readonly IAPIMethodWrapperService _methodWrapepr;

    public ProjectController(IMapper mapper, ApplicationDbContext context, IDbLookupService lookup, IAPIMethodWrapperService amws)
    {
        _mapper = mapper;
        _context = context;
        _lookup = lookup;
        _methodWrapepr = amws;
    }
    [HttpGet]
    public IActionResult GetAllProjects() =>
        Ok(
            _context.Projects!.Include(project => project.Customer).Select(project =>
               _mapper.Map<ProjectGetAllDTO>(project)
            )
        );
    [HttpGet]
    [Route("{Id}")]
    public IActionResult GetProjectById(int Id)
    {
        var (status, project) = _lookup.VerifyItemID(Id, nameof(Project.ProjectId), _context.Projects!.Include(project => project.Customer).ToList());

        return (status == ItemExistStatus.ItemDoesNotExist) ?
            NotFound() :
            Ok(
                _mapper.Map<ProjectGetOneDTO>(project)
            );
    }
    [HttpPost]
    public IActionResult AddNewProject(ProjectPostDTO ppd)
    {
        var newProject = _mapper.Map<Project>(ppd);

        _context.Projects!.Add(newProject);
        _context.SaveChanges();

        return CreatedAtAction(
            nameof(GetProjectById),
            new { Id = newProject.ProjectId },
            _mapper.Map<ProjectGetOneDTO>(newProject)
        );
    }
    [HttpPut]
    [Route("{Id}")]
    public IActionResult ReplaceProjectByID(int Id, ProjectPutDTO cpd)
    {
        var (status, projectToEdit) = _lookup.VerifyItemID(Id, nameof(Project.ProjectId), _context.Projects!.ToList());
        return (status == ItemExistStatus.ItemDoesNotExist) ?
            NotFound() :
            _methodWrapepr.NonSafeHTTPMEthodWrapper(
                () => _mapper.Map(cpd, projectToEdit)
            );
    }
    [HttpDelete]
    [Route("{Id}")]
    public IActionResult DeleteProjectByID(int Id)
    {
        var (status, projectToRemove) = _lookup.VerifyItemID(Id, nameof(Project.ProjectId), _context.Projects!.ToList());
        return (status.Equals(ItemExistStatus.ItemDoesNotExist)) ?
            NotFound() :
            _methodWrapepr.NonSafeHTTPMEthodWrapper(
                () => _context.Projects!.Remove(projectToRemove)
            );
    }
    [HttpPatch]
    [Route("{Id}")]
    public IActionResult UpdateProjectPropertyByID(int Id, [FromBody] JsonPatchDocument<Project> projectEntity)
    {
        var (status, projectToPatch) = _lookup.VerifyItemID(Id, nameof(Project.ProjectId), _context.Projects!.ToList());
        return (status == ItemExistStatus.ItemDoesNotExist) ?
            NotFound() :
            _methodWrapepr.NonSafeHTTPMEthodWrapper(
                () => projectEntity.ApplyTo(projectToPatch)
            );
    }
}