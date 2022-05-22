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
using WebAPI.DTO.TidsRegistrering;
using WebAPI.Infrastructure.Profiles;
using WebAPI.Services;

namespace SystemTests.WebAPI.Controllers;

[TestClass]
public class TidsRegistreringControllerTest
{
    private readonly ApplicationDbContext _context;
    private readonly CreateUniqeService _creator;
    private readonly TidsRegistreringController _sut;
    private readonly ITestAPIService _tester;

    public string TestCustomerName { get; set; }
    public string TestProjectName { get; set; }

    public TidsRegistreringControllerTest()
    {
        _tester = new TestAPIService();
        _context = TestDatabaseService.CreateTestContext(nameof(TidsRegistreringControllerTest));
        _creator = new CreateUniqeService();

        TestCustomerName = Guid.NewGuid().ToString();
        TestProjectName = Guid.NewGuid().ToString();

        TestDatabaseService.AddCustomer(TestCustomerName, _creator, _context);
        TestDatabaseService.AddProject(TestProjectName, _context.Customers!.Last().CustomerId, _creator, _context);
        TestDatabaseService.AddTidsRegistrering(
            Guid.NewGuid().ToString(),
            new Random().Next(1, 1440),
            _context.Projects!.First().ProjectId,
            DateTime.Now,
            _creator,
            _context
        );

        _sut = createAPI();
    }
    private TidsRegistreringController createAPI()
    {
        var conf = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TidsRegistreringProfile>();
        }
        );
        var app = new TidsRegistreringController(new Mapper(conf), _context, new DbLookupService(), new APIMethodWrapperService(_context));
        return app;
    }
    //HTTP GET
    [TestMethod]
    public void When_Call_Get_Method_All_Items_Should_Return()
    {
        var returnCodeCompare = StatusCodes.Status200OK;
        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.GetAllRegs(),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    //HTTP GET/ID
    [TestMethod]
    public void When_Call_Get_Single_Method_With_Valid_Id()
    {
        var returnCodeCompare = StatusCodes.Status200OK;
        var existingItem = _context.TidsRegistrerings!.First();

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.GetRegById(existingItem.TidsRegistreringId),
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
                () => _sut.GetRegById(nonExistingID),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    //HTTP POST
    [TestMethod]
    public void When_Call_Post_Single_Method_With_New_Item()
    {
        var returnCodeCompare = StatusCodes.Status201Created;

        var description = Guid.NewGuid().ToString();
        var projectId = _context.Projects!.Last().CustomerId;
        var antalMinuter = new Random().Next(1, 1440);
        var date = DateTime.Now;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.AddNewReg(
                    new TidsRegistreringPostDTO
                    {
                        Beskrivning = description,
                        Datum = date,
                        AntalMinuter = antalMinuter,
                        ProjectId = projectId
                    }
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    [TestMethod]
    public void When_Correct_Values_Are_Posted_Should_Succeed()
    {
        var description = Guid.NewGuid().ToString();
        var projectId = _context.Projects!.Last().CustomerId;
        var antalMinuter = new Random().Next(1, 1440);
        var date = DateTime.Now;

        _sut.AddNewReg(
            new TidsRegistreringPostDTO
            {
                Beskrivning = description,
                Datum = date,
                AntalMinuter = antalMinuter,
                ProjectId = projectId
            }
        );

        var AddedItem = _context.TidsRegistrerings!.First(reg => reg.Beskrivning == description);

        Assert.IsNotNull(AddedItem);
        Assert.AreEqual(description, AddedItem.Beskrivning);
        Assert.AreEqual(projectId, AddedItem.ProjectId);
        Assert.AreEqual(antalMinuter, AddedItem.AntalMinuter);
        Assert.AreEqual(date, AddedItem.Datum);
    }
    //HTTP PUT
    [TestMethod]
    public void When_Call_Put_With_Invalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status404NotFound;
        var nonExistingID = -1;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.ReplaceRegByID(nonExistingID, null!),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    [TestMethod]
    public void When_Call_Put_With_Valalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status204NoContent;

        var regId = _context.TidsRegistrerings!.Last().TidsRegistreringId;

        var description = Guid.NewGuid().ToString();
        var antalMinuter = new Random().Next(1, 1440);
        var date = DateTime.Now;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.ReplaceRegByID(
                    regId,
                    new TidsRegistreringPutDTO
                    {
                        Beskrivning = description,
                        Datum = date,
                        AntalMinuter = antalMinuter
                    }
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    [TestMethod]
    public void When_Correct_Values_Are_Put_Should_Succeed()
    {
        var regId = _context.TidsRegistrerings!.Last().TidsRegistreringId;

        var description = Guid.NewGuid().ToString();
        var antalMinuter = new Random().Next(1, 1440);
        var date = DateTime.Now;

        _sut.ReplaceRegByID(
            regId,
            new TidsRegistreringPutDTO
            {
                Beskrivning = description,
                Datum = date,
                AntalMinuter = antalMinuter
            }
        );

        var EditedReg = _context.TidsRegistrerings!.First(reg => reg.Beskrivning == description);
        
        Assert.IsNotNull(EditedReg);
        Assert.AreEqual(description, EditedReg.Beskrivning);
        Assert.AreEqual(antalMinuter, EditedReg.AntalMinuter);
        Assert.AreEqual(date, EditedReg.Datum);
    }
    //HTTP DELETE
    [TestMethod]
    public void When_Call_Delete_With_Invalalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status404NotFound;
        var nonExistingId = -1;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.DeleteRegByID(
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
        var existingID = _context.TidsRegistrerings!.First().TidsRegistreringId;

        Assert.AreEqual(
            _tester.APITestResponseCode(
                () => _sut.DeleteRegByID(
                    existingID
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            ), true
        );
    }
    [TestMethod]
    public void When_Correct_ID_Is_Given_Should_Not_Exist()
    {
        var RegIDToRemove = _context.TidsRegistrerings!.First().TidsRegistreringId;

        _sut.DeleteRegByID(
            RegIDToRemove
        );

        var RemovedItem = _context.TidsRegistrerings!.FirstOrDefault(reg => reg.TidsRegistreringId == RegIDToRemove);

        Assert.AreEqual(default(TidsRegistrering), RemovedItem);
    }
    //HTTP PATCH
    [TestMethod]
    public void When_Call_Patch_With_Invalalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status404NotFound;
        var nonExistingId = -1;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.UpdateRegPropertyByID(
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
        var existingID = _context.TidsRegistrerings!.First().TidsRegistreringId;

        var body = new JsonPatchDocument<TidsRegistrering>();
        body.Replace(reg => reg.Beskrivning, "This is the new Value");

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.UpdateRegPropertyByID(
                    existingID,
                    body
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
}