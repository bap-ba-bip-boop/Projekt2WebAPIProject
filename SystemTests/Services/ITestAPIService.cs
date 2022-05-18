using Microsoft.AspNetCore.Mvc;
using System;

namespace SystemTests.Services;

public interface ITestAPIService
{
    bool APITestResponseCode<ReturnType>(Func<ReturnType> ActAction, Func<ReturnType, bool> AssertAction);
    bool DefaultAPIResponseCodeCheck(IActionResult response, int returnCodeCompare);
}
