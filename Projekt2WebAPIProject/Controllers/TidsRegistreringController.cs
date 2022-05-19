using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedResources.Services;
using WebAPI.DTO.Project;
using WebAPI.DTO.TidsRegistrering;
using WebAPI.Model;
using WebAPI.Services;
using static SharedResources.Services.IDbLookupService;

namespace WebAPI.Controllers;

[ApiController]
[Route("tidsregistrering")]
public class TidsRegistreringController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly APIDbContext _context;
    private readonly IDbLookupService _lookup;
    private readonly IAPIMethodWrapperService _methodWrapepr;

    public TidsRegistreringController(IMapper mapper, APIDbContext context, IDbLookupService lookup, IAPIMethodWrapperService amws)
    {
        _mapper = mapper;
        _context = context;
        _lookup = lookup;
        _methodWrapepr = amws;
    }
    [HttpGet]
    public IActionResult GetAllRegs() =>
        Ok(
            _context.TidsRegistrerings!
                .Include(reg => reg.Project)
                .Select(reg =>
               _mapper.Map<TidsRegistreringGetAllDTO>(reg)
            ).ToList()
            .OrderByDescending(reg => reg.Datum)
        );
    [HttpGet]
    [Route("{Id}")]
    public IActionResult GetRegById(int Id)
    {
        var (status, reg) = _lookup.VerifyItemID(
            Id,
            nameof(TidsRegistrering.TidsRegistreringId),
            _context.TidsRegistrerings!.Include(reg => reg.Project)
            .ThenInclude(proj => proj!.Customer)
            .ToList()
        );

        return (status == ItemExistStatus.ItemDoesNotExist) ?
            NotFound() :
            Ok(
                _mapper.Map<TidsRegistreringGetOneDTO>(reg)
            );
    }
    [HttpPost]
    public IActionResult AddNewReg(TidsRegistreringPostDTO trpd)
    {
        var newReg = _mapper.Map<TidsRegistrering>(trpd);

        _context.TidsRegistrerings!.Add(newReg);
        _context.SaveChanges();

        return CreatedAtAction(
            nameof(GetRegById),
            new { Id = newReg.TidsRegistreringId },
            _mapper.Map<TidsRegistreringGetOneDTO>(newReg)
        );
    }
    [HttpPut]
    [Route("{Id}")]
    public IActionResult ReplaceRegByID(int Id, TidsRegistreringPutDTO trpd)
    {
        var (status, regToEdit) = _lookup.VerifyItemID(
            Id,
            nameof(TidsRegistrering.TidsRegistreringId),
            _context.TidsRegistrerings!
            .ToList()
        );
        return (status == ItemExistStatus.ItemDoesNotExist) ?
            NotFound() :
            _methodWrapepr.NonSafeHTTPMEthodWrapper(
                () => _mapper.Map(trpd, regToEdit)
            );
    }
    [HttpDelete]
    [Route("{Id}")]
    public IActionResult DeleteRegByID(int Id)
    {
        var (status, regToRemove) = _lookup.VerifyItemID(
            Id,
            nameof(TidsRegistrering.TidsRegistreringId),
            _context.TidsRegistrerings!
            .ToList()
        );
        return (status.Equals(ItemExistStatus.ItemDoesNotExist)) ?
            NotFound() :
            _methodWrapepr.NonSafeHTTPMEthodWrapper(
                () => _context.TidsRegistrerings!.Remove(regToRemove)
            );
    }
    [HttpPatch]
    [Route("{Id}")]
    public IActionResult UpdateRegPropertyByID(int Id, [FromBody] JsonPatchDocument<TidsRegistrering> projectEntity)
    {
        var (status, regToPatch) = _lookup.VerifyItemID(
            Id,
            nameof(TidsRegistrering.TidsRegistreringId),
            _context.TidsRegistrerings!
            .ToList()
        );
        return (status == ItemExistStatus.ItemDoesNotExist) ?
            NotFound() :
            _methodWrapepr.NonSafeHTTPMEthodWrapper(
                () => projectEntity.ApplyTo(regToPatch)
            );
    }
}