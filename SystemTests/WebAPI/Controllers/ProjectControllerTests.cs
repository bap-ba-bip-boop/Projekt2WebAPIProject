using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Data;
using SharedResources.Services;
using System;
using System.Linq;
using SystemTests.Services;
using WebAPI.Controllers;
using WebAPI.DTO.Project;
using WebAPI.Infrastructure.Profiles;
using WebAPI.Services;

namespace SystemTests.WebAPI.Controllers;

[TestClass]
public class ProjectControllerTest
{
    private readonly ApplicationDbContext _context;
    private readonly CreateUniqeService _creator;
    private readonly ProjectController _sut;
    private readonly ITestAPIService _tester;

    public ProjectControllerTest()
    {
        _tester = new TestAPIService();
        _context = TestDatabaseService.CreateTestContext(nameof(ProjectControllerTest));
        _creator = new CreateUniqeService();

        _sut = createAPI();
    }
    private ProjectController createAPI()
    {
        var conf = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProjectProfile>();
        }
        );
        var app = new ProjectController(new Mapper(conf), _context, new DbLookupService(), new APIMethodWrapperService(_context));
        return app;
    }
    //HTTP GET
    [TestMethod]
    public void When_Call_Get_Method_All_Items_Should_Return()
    {
        var returnCodeCompare = StatusCodes.Status200OK;
        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.GetAllProjects(),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    //HTTP GET/ID
    [TestMethod]
    public void When_Call_Get_Single_Method_With_Valid_Id()
    {
        var TestCustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(TestCustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == TestCustomerName).CustomerId;

        var TestProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(TestProjectName, CustomerId, _creator, _context);
        var existingItem = _context.Projects!.First(proj => proj.ProjectName == TestProjectName);

        var returnCodeCompare = StatusCodes.Status200OK;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.GetProjectById(existingItem.ProjectId),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );

    }
    [TestMethod]
    public void When_Call_Get_Single_Method_With_Invalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status404NotFound;
        var nonExistingID = -1;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.GetProjectById(nonExistingID),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
}