using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

namespace SystemTests.WebAPI.Controllers;

[TestClass]
public class CustomerControllerTest
{
    private readonly APIDbContext _context;
    private readonly CreateUniqeService _creator;
    private readonly CusotmerController _sut;
    private readonly ITestAPIService _tester;

    public string TestName { get; set; }

    public CustomerControllerTest()
    {
        _tester = new TestAPIService();
        _context = TestDatabaseService.CreateTestContext(nameof(CustomerControllerTest));
        _creator = new CreateUniqeService();

        TestName = Guid.NewGuid().ToString();

        TestDatabaseService.AddCustomer(TestName, _creator, _context);

        _sut = createAPI();
    }
    private CusotmerController createAPI()
    {
        var conf = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
        }
        );
        var app = new CusotmerController(new Mapper(conf), _context, new DbLookupService(), new APIMethodWrapperService(_context));
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
                () => _sut.GetCustomerById(existingItem.CustomerId),
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
    [TestMethod]
    public void When_Correct_Values_Are_Posted_Should_Succeed()
    {
        var NameToAdd = Guid.NewGuid().ToString();

        _sut.AddNewCustomer(
            new CustomerPostDTO
            {
                Name = NameToAdd
            }
        );

        var AddedItem = _context.Customers!.First(customer => customer.Name == NameToAdd);

        Assert.IsNotNull(AddedItem);
        Assert.AreEqual(NameToAdd, AddedItem.Name);
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
        var existingID = existingItem.CustomerId;

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
    [TestMethod]
    public void When_Correct_Values_Are_Put_Should_Succeed()
    {
        string NameToEdit = Guid.NewGuid().ToString();
        var CustomerIDToReplace = _context.Customers!.First().CustomerId;

        _sut.ReplaceCustomerByID(
            CustomerIDToReplace,
            new CustomerPutDTO
            {
                Name = NameToEdit
            }
        );

        var EditedCustomer = _context.Customers!.First(customer => customer.CustomerId == CustomerIDToReplace);

        Assert.IsNotNull(EditedCustomer);
        Assert.AreEqual(NameToEdit, EditedCustomer.Name);
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
        var existingID = _context.Customers!.First().CustomerId;

        Assert.AreEqual(
            _tester.APITestResponseCode(
                () => _sut.DeleteCustomerByID(
                    existingID
                ),
                response => _tester.DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            ), true
        );
    }
    [TestMethod]
    public void When_Correct_ID_Is_Given_Should_Not_Exist()
    {
        var CustomerIDToRemove = _context.Customers!.First().CustomerId;

        _sut.DeleteCustomerByID(
            CustomerIDToRemove
        );

        var RemovedItem = _context.Customers!.FirstOrDefault(customer => customer.CustomerId == CustomerIDToRemove);

        Assert.AreEqual(default(Customer), RemovedItem);
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
        var existingID = _context.Customers!.First().CustomerId;

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