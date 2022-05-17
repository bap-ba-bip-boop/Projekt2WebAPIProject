using System;
using Microsoft.AspNetCore.Mvc;

namespace SystemTests.Services;

public class TestAPIService : ITestAPIService
{
    public bool DefaultAPIResponseCodeCheck(IActionResult response, int returnCodeCompare)
    {
        var StatusCode = 0;
        var prop = response.GetType().GetProperty(nameof(StatusCode));
        StatusCode = (int)prop!.GetValue(response)!;
        return returnCodeCompare.Equals(StatusCode);
    }
    public bool APITestResponseCode<ReturnType>(Func<ReturnType> ActAction, Func<ReturnType, bool> AssertAction) =>
        AssertAction(ActAction());
}