
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Services
{
    public interface IAPIMethodWrapperService
    {
        NotFoundResult NonSafeHTTPMEthodWrapper(Action action);
    }
}