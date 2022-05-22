using Microsoft.AspNetCore.Mvc;
using SharedResources.Data;

namespace WebAPI.Services;

public class APIMethodWrapperService : IAPIMethodWrapperService
{
    private readonly ApplicationDbContext _context;

    public APIMethodWrapperService(ApplicationDbContext context)
    {
        _context = context;
    }
    public NoContentResult NonSafeHTTPMEthodWrapper(Action action)
    {
        action();
        _context.SaveChanges();
        return new NoContentResult();
    }
}
