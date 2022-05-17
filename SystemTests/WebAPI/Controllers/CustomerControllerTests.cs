using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Services;
using System;
using System.Linq;
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

    public string TestName { get; set; }

    public AdsControllerTest()
    {
        var options = new DbContextOptionsBuilder<APIDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;
        _context = new APIDbContext(options);
        _creator = new CreateUniqeService();

        TestName = Guid.NewGuid().ToString();

        addAdvertisement(TestName);

        _sut = createAPI();
    }
    private CreateUniqueStatus addAdvertisement(string name) =>
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
    private bool DefaultAPIResponseCodeCheck(IActionResult response, int returnCodeCompare)
    {
        var StatusCode = ((StatusCodeResult)response).StatusCode;
        //var prop = response.GetType().GetProperty(nameof(StatusCode));
        //StatusCode = (int)prop!.GetValue(response)!;
        return returnCodeCompare.Equals(StatusCode);
    }
    private bool APITestResponseCode<ReturnType>(Func<ReturnType> ActAction, Func<ReturnType, bool> AssertAction) =>
        AssertAction(ActAction());
    //HTTP GET
    [TestMethod]
    public void When_Call_Get_Method_All_Items_Should_Return()
    {
        //Arrange
        var returnCodeCompare = StatusCodes.Status200OK;
        //Act
        //Assert
        Assert.IsTrue(
            APITestResponseCode(
                () => _sut.GetAllCustomers(),
                response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
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
            APITestResponseCode(
                () => _sut.GetCustomerById(existingItem.Id),
                response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );

    }
    [TestMethod]
    public void When_Call_Get_Single_Method_With_Invalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status404NotFound;
        var nonExistingID = -1;

        Assert.IsTrue(
            APITestResponseCode(
                () => _sut.GetCustomerById(nonExistingID),
                response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
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
            APITestResponseCode(
                () => _sut.AddNewCustomer(
                    new CustomerPostDTO
                    {
                        Name = name
                    }
                ),
                response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
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
            APITestResponseCode(
                () => _sut.ReplaceCustomerByID(nonExistingID, null!),
                response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
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
            APITestResponseCode(
                () => _sut.ReplaceCustomerByID(
                    existingID,
                    new CustomerPutDTO
                    {
                        Name = existingItem.Name
                    }
                ),
                response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
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
            APITestResponseCode(
                () => _sut.DeleteCustomerByID(
                    nonExistingId
                ),
                response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            )
        );
    }
    [TestMethod]
    public void When_Call_Delete_With_Valalid_Id()
    {
        var returnCodeCompare = StatusCodes.Status204NoContent;
        var existingID = _context.Customers!.First().Id;

        Assert.AreEqual(// det som händer ät att ASSERT får ett false värde?
            APITestResponseCode(
                () => _sut.DeleteCustomerByID(
                    existingID
                ),
                response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
            ), true
        );
    }
    //HTTP PATCH
    //[TestMethod]
    //public void When_Call_Patch_With_Invalalid_Id()
    //{
    //    var returnCodeCompare = StatusCodes.Status404NotFound;
    //    var nonExistingId = -1;
    //
    //    Assert.IsTrue(
    //        APITestResponseCode(
    //            () => _sut.PartialUpdateAdvertisement(
    //                nonExistingId,
    //                null
    //            ),
    //            response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
    //        )
    //    );
    //}
    //[TestMethod]
    //public void When_Call_Patch_With_Valalid_Id()
    //{
    //    var returnCodeCompare = StatusCodes.Status204NoContent;
    //    var existingID = _context.advertisements.First().Id;
    //
    //    var body = new JsonPatchDocument<Advertisement>();
    //    body.Replace(Ad => Ad.FillerText, "This is the new Value");
    //
    //    Assert.IsTrue(
    //        APITestResponseCode(
    //            () => _sut.PartialUpdateAdvertisement(
    //                existingID,
    //                body
    //            ),
    //            response => DefaultAPIResponseCodeCheck(response, returnCodeCompare)
    //        )
    //    );
    //}
}