using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPI.Services;

public class APIMethodWrapperService : IAPIMethodWrapperService
{
    private readonly APIDbContext _context;

    public APIMethodWrapperService(APIDbContext context)
    {
        _context = context;
    }
    public NoContentResult NonSafeHTTPMEthodWrapper(Action action)
    {
        action();
        _context.SaveChanges();
        return new NoContentResult();//StatusCodes.Status204NoContent;
    }
}
