using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Services;
using System;
using System.Linq;
using SystemTests.Services;
using WebAPI.Controllers;
using WebAPI.DTO.Project;
using WebAPI.Infrastructure.Profiles;
using WebAPI.Model;
using WebAPI.Services;
using static SharedResources.Services.ICreateUniqeService;

namespace SystemTests.WebAPI.Controllers;

[TestClass]
public class ProjectControllerTest
{
    private readonly APIDbContext _context;
    private readonly CreateUniqeService _creator;
    private readonly ProjectController _sut;
    private readonly ITestAPIService _tester;

    public string TestName { get; set; }

    public ProjectControllerTest()
    {
        _tester = new TestAPIService();
        _context = TestDatabaseService.CreateTestContext(nameof(ProjectControllerTest));
        _creator = new CreateUniqeService();

        TestName = Guid.NewGuid().ToString();

        addProject(TestName, 1);// change change change

        _sut = createAPI();
    }
    private CreateUniqueStatus addProject(string name, int customerId) =>
        _creator.CreateIfNotExists(
            _context,
            _context.Projects!,
            item => item.ProjectName!.Equals(name),
            new Project
            {
                ProjectName = name,
                CustomerId = customerId
            }
        );
    private ProjectController createAPI()
    {
        var conf = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProjectProfile>();
        }
        );
        var app = new ProjectController( new Mapper(conf), _context, new DbLookupService(), new APIMethodWrapperService(_context));
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
        var returnCodeCompare = StatusCodes.Status200OK;
        var existingItem = _context.Projects!.First();

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
        var nonExistingID = -1;// change change change

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.GetProjectById(nonExistingID),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    //HTTP POST
    [TestMethod]
    public void When_Call_Post_Single_Method_With_New_Ad()
    {
        var returnCodeCompare = StatusCodes.Status201Created;
        var name = Guid.NewGuid().ToString();
        var customerId = 1;// change change change

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.AddNewProject(
                    new ProjectPostDTO
                    {
                        ProjectName = name,
                        CustomerId = customerId
                    }
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    //HTTP PUT
    [TestMethod]
    public void When_Call_Put_With_Invalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status404NotFound;
        var nonExistingID = -1;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.ReplaceProjectByID(nonExistingID, null!),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    [TestMethod]
    public void When_Call_Put_With_Valalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status204NoContent;
        var existingItem = _context.Projects!.First();
        var existingID = existingItem.ProjectId;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.ReplaceProjectByID(
                    existingID,
                    new ProjectPutDTO
                    {
                        ProjectName = existingItem.ProjectName,
                        CustomerId = existingItem.CustomerId
                    }
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    //HTTP DELETE
    [TestMethod]
    public void When_Call_Delete_With_Invalalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status404NotFound;
        var nonExistingId = -1;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.DeleteProjectByID(
                    nonExistingId
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    [TestMethod]
    public void When_Call_Delete_With_Valalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status204NoContent;
        var existingID = _context.Projects!.First().ProjectId;

        Assert.AreEqual(
            _tester.APITestResponseCode(
                () => _sut.DeleteProjectByID(
                    existingID
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            ), true
        );
    }
    //HTTP PATCH
    [TestMethod]
    public void When_Call_Patch_With_Invalalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status404NotFound;
        var nonExistingId = -1;
    
        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.UpdateProjectPropertyByID(
                    nonExistingId,
                    null!
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    [TestMethod]
    public void When_Call_Patch_With_Valalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status204NoContent;
        var existingID = _context.Projects!.First().ProjectId;
    
        var body = new JsonPatchDocument<Project>();
        body.Replace(project => project.ProjectName, "This is the new Value");
    
        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.UpdateProjectPropertyByID(
                    existingID,
                    body
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
}