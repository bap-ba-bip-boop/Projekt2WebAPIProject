
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Services
{
    public interface IAPIMethodWrapperService
    {
        public NoContentResult NonSafeHTTPMEthodWrapper(Action action);
    }
}