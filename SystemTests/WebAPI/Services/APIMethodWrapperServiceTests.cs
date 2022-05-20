using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Data;
using SystemTests.Services;
using WebAPI.Model;
using WebAPI.Services;

namespace SystemTests.WebAPI.Services;

[TestClass]
public class APIMethodWrapperServiceTests
{
    private readonly APIMethodWrapperService _sut;
    private readonly ApplicationDbContext _context;
    public APIMethodWrapperServiceTests()
    {
        _context = TestDatabaseService.CreateTestContext(nameof(APIMethodWrapperServiceTests));
        _sut = new(_context!);
    }
    private bool DefaultAPIResponseCodeCheck(IActionResult response, int returnCodeCompare) =>
        returnCodeCompare.Equals(((StatusCodeResult)response).StatusCode);
    [TestMethod]
    public void Should_Return_204NoContent()
    {
        var responseCode = StatusCodes.Status204NoContent;

        Assert.IsTrue(DefaultAPIResponseCodeCheck(_sut.NonSafeHTTPMEthodWrapper(() => { }), responseCode));
    }
}
