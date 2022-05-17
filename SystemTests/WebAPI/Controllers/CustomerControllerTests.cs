using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Services;
using System;
using System.Linq;
using SystemTests.Services;
using WebAPI.Controllers;
using WebAPI.DTO.Customer;
using WebAPI.Infrastructure.Profiles;
using WebAPI.Model;
using WebAPI.Services;
using static SharedResources.Services.ICreateUniqeService;

namespace SystemTests.WebAPI.Controllers;

[TestClass]
public class AdsControllerTest
{
    private readonly APIDbContext _context;
    private readonly CreateUniqeService _creator;
    private readonly CusotmerController _sut;
    private readonly ITestAPIService _tester;

    public string TestName { get; set; }

    public AdsControllerTest()
    {
        _tester = new TestAPIService();
        var options = new DbContextOptionsBuilder<APIDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;
        _context = new APIDbContext(options);
        _creator = new CreateUniqeService();

        TestName = Guid.NewGuid().ToString();

        addCustomer(TestName);

        _sut = createAPI();
    }
    private CreateUniqueStatus addCustomer(string name) =>
        _creator.CreateIfNotExists(
            _context,
            _context.Customers!,
            item => item.Name!.Equals(name),
            new Customer
            {
                Name = name
            }
        );
    private CusotmerController createAPI()
    {
        var conf = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
        }
        );
        var app = new CusotmerController( new Mapper(conf), _context, new DbLookupService(), new APIMethodWrapperService(_context));
        return app;
    }
    //HTTP GET
    [TestMethod]
    public void When_Call_Get_Method_All_Items_Should_Return()
    {
        var returnCodeCompare = StatusCodes.Status200OK;
        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.GetAllCustomers(),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    //HTTP GET/ID
    [TestMethod]
    public void When_Call_Get_Single_Method_With_Valid_Id()
    {
        var returnCodeCompare = StatusCodes.Status200OK;
        var existingItem = _context.Customers!.First();

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.GetCustomerById(existingItem.Id),
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
                () => _sut.GetCustomerById(nonExistingID),
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

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.AddNewCustomer(
                    new CustomerPostDTO
                    {
                        Name = name
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
                () => _sut.ReplaceCustomerByID(nonExistingID, null!),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    [TestMethod]
    public void When_Call_Put_With_Valalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status204NoContent;
        var existingItem = _context.Customers!.First();
        var existingID = existingItem.Id;

        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.ReplaceCustomerByID(
                    existingID,
                    new CustomerPutDTO
                    {
                        Name = existingItem.Name
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
                () => _sut.DeleteCustomerByID(
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
        var existingID = _context.Customers!.First().Id;

        Assert.AreEqual(
            _tester.APITestResponseCode(
                () => _sut.DeleteCustomerByID(
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
                () => _sut.UpdateCustomerPropertyByID(
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
        var existingID = _context.Customers!.First().Id;
    
        var body = new JsonPatchDocument<Customer>();
        body.Replace(customer => customer.Name, "This is the new Value");
    
        Assert.IsTrue(
            _tester.APITestResponseCode(
                () => _sut.UpdateCustomerPropertyByID(
                    existingID,
                    body
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
}