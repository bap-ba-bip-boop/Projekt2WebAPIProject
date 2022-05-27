using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;
using SharedResources.Services;
using WebAPI.DTO.Project;
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
}