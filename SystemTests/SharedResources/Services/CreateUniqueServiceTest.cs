using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Services;
using System;
using WebAPI.Model;
using static SharedResources.Services.ICreateUniqeService;

namespace SystemTests.SharedResources;

[TestClass]
public class CreateUniqueServiceTest
{
    public string TestName { get; set; }

    private readonly APIDbContext _context;

    private readonly ICreateUniqeService _sut;
    public CreateUniqueServiceTest()
    {
        var options = new DbContextOptionsBuilder<APIDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;
        _context = new APIDbContext(options);
        _sut = new CreateUniqeService();

        TestName= Guid.NewGuid().ToString();

        _ = addAdvertisement(TestName);

    }
    private CreateUniqueStatus addAdvertisement(string name) =>
        _sut.CreateIfNotExists(
            _context,
            _context.Customers!,
            item => item.Name!.Equals(name),
            new Customer{
                Name = name,
            }
        );
    [TestMethod]
    public void When_Ad_Exists_Should_Return_AlreadyExists()
    {
        //Arrange
        var name = TestName;

        //Act
        var returnStatus = addAdvertisement(name);

        //Assert
        Assert.IsTrue(returnStatus == CreateUniqueStatus.AlreadyExists);
    }
    [TestMethod]
    public void When_Ad_Dont_Exists_Should_Return_Ok()
    {
        //Arrange
        var title = Guid.NewGuid().ToString();

        //Act
        var returnStatus = addAdvertisement(title);

        //Assert
        Assert.IsTrue(returnStatus == CreateUniqueStatus.Ok);
    }
}