using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Model;
using WebAPI.Services;

namespace SystemTests.WebAPI.Services;

[TestClass]
public class APIMethodWrapperServiceTests
{
    private readonly APIMethodWrapperService _sut;
    private readonly APIDbContext _context;
    public APIMethodWrapperServiceTests()
    {
        var options = new DbContextOptionsBuilder<APIDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;
        _context = new APIDbContext(options);
        _sut = new(_context!);
    }
    private bool DefaultAPIResponseCodeCheck(IActionResult response, int returnCodeCompare) =>
        returnCodeCompare.Equals( ((StatusCodeResult)response).StatusCode );
    [TestMethod]
    public void Should_Return_204NoContent()
    {
        var responseCode = StatusCodes.Status204NoContent;

        Assert.IsTrue( DefaultAPIResponseCodeCheck(_sut.NonSafeHTTPMEthodWrapper( () => {}), responseCode ) );
    }
}
