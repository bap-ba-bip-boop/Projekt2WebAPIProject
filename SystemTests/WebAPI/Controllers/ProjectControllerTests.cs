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
    //HTTP POST
    [TestMethod]
    public void When_Call_Post_Single_Method_With_New_Ad()
    {
        
        var returnCodeCompare = StatusCodes.Status201Created;
        var name = Guid.NewGuid().ToString();
        var customerId = _context.Customers!.Last().CustomerId;

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
    [TestMethod]
    public void When_Correct_Values_Are_Posted_Should_Succeed()
    {
        var NameToAdd = Guid.NewGuid().ToString();
        var projOwnerId = _context.Customers!.First().CustomerId;

        _sut.AddNewProject(
            new ProjectPostDTO
            {
                ProjectName = NameToAdd,
                CustomerId = projOwnerId
            }
        );

        var AddedItem = _context.Projects!.First(project => project.ProjectName == NameToAdd);

        Assert.IsNotNull(AddedItem);
        Assert.AreEqual(NameToAdd, AddedItem.ProjectName);
        Assert.AreEqual(projOwnerId, AddedItem.CustomerId);
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
        var TestCustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(TestCustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == TestCustomerName).CustomerId;

        var TestProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(TestProjectName, CustomerId, _creator, _context);
        var existingItem = _context.Projects!.First(proj => proj.ProjectName == TestProjectName);
        var existingID = existingItem.ProjectId;

        var returnCodeCompare = StatusCodes.Status204NoContent;

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
    [TestMethod]
    public void When_Correct_Values_Are_Put_Should_Succeed()
    {
        var TestCustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(TestCustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == TestCustomerName).CustomerId;

        var TestProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(TestProjectName, CustomerId, _creator, _context);
        var existingItem = _context.Projects!.First(proj => proj.ProjectName == TestProjectName);
        var existingID = existingItem.ProjectId;

        string NameToEdit = Guid.NewGuid().ToString();

        var ProjectIdToReplace = existingID;

        _sut.ReplaceProjectByID(
            ProjectIdToReplace,
            new ProjectPutDTO
            {
                ProjectName = NameToEdit,
                CustomerId = existingID
            }
        );

        var EditedProject = _context.Projects!.First(project => project.ProjectId == ProjectIdToReplace);

        Assert.IsNotNull(EditedProject);
        Assert.AreEqual(NameToEdit, EditedProject.ProjectName);
        Assert.AreEqual(existingID, EditedProject.CustomerId);
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
        var TestCustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(TestCustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == TestCustomerName).CustomerId;

        var TestProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(TestProjectName, CustomerId, _creator, _context);
        var existingItem = _context.Projects!.First(proj => proj.ProjectName == TestProjectName);
        var existingID = existingItem.ProjectId;

        var returnCodeCompare = StatusCodes.Status204NoContent;
        //var existingID = _context.Projects!.First().ProjectId;

        Assert.AreEqual(
            _tester.APITestResponseCode(
                () => _sut.DeleteProjectByID(
                    existingID
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            ), true
        );
    }
    [TestMethod]
    public void When_Correct_ID_Is_Given_Should_Not_Exist()
    {
        var TestCustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(TestCustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == TestCustomerName).CustomerId;

        var TestProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(TestProjectName, CustomerId, _creator, _context);
        var existingItem = _context.Projects!.First(proj => proj.ProjectName == TestProjectName);
        var existingID = existingItem.ProjectId;
        //var ProjectIDToRemove = _context.Projects!.First().ProjectId;

        _sut.DeleteProjectByID(
            existingID
        );

        var RemovecItem = _context.Projects!.FirstOrDefault(customer => customer.ProjectId == existingID);

        Assert.AreEqual(default(Project), RemovecItem);
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
        var TestCustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(TestCustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == TestCustomerName).CustomerId;

        var TestProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(TestProjectName, CustomerId, _creator, _context);
        var existingItem = _context.Projects!.First(proj => proj.ProjectName == TestProjectName);
        var existingID = existingItem.ProjectId;

        var returnCodeCompare = StatusCodes.Status204NoContent;
        //var existingID = _context.Projects!.First().ProjectId;

        var body = new JsonPatchDocument<Project>();
        body.Replace(project => project.ProjectName, Guid.NewGuid().ToString());

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