
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using SharedResources.Services;
using WebAPI.Model;
using System;
using System.Linq;
using static SharedResources.Services.IDbLookupService;

namespace SystemTests.Inlämning_API.Services;

[TestClass]
public class DbLookupServiceTest
{

    private readonly APIDbContext _context;
    private readonly DbLookupService _sut;
    private readonly ICreateUniqeService _creator;
    public DbLookupServiceTest()
    {
        var options = new DbContextOptionsBuilder<APIDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;
        _context = new APIDbContext(options);

        _creator = new CreateUniqeService();

        addItem(Guid.NewGuid().ToString());

        _sut = new DbLookupService();
    }
    private void addItem(string name) =>
        _creator.CreateIfNotExists(
            _context,
            _context.Customers!,
            item => item.Name!.Equals(name),
            new Customer{
                Name=name
            }
        );

    [TestMethod]
    public void When_Account_Exists_Should_Return_AdExists()
    {
        //Arrange
        var existingItem = _context.Customers!.First();

        //Act
        var (returnStatus, returnCustomer) = _sut.VerifyItemID(existingItem.Id,nameof(existingItem.Id), _context.Customers!);

        //Assert
        Assert.IsTrue(returnStatus == ItemExistStatus.ItemExists);
        Assert.IsFalse(returnCustomer == null);
    }
    [TestMethod]
    public void When_Account_Dont_Exist_Should_Return_AdDoesNotExist()
    {
        //Arrange
        var nonExistingAccount = new Customer{
            Id = -1,
            Name="name"
        };

        //Act
        var (returnStatus, returnAd) = _sut.VerifyItemID(nonExistingAccount.Id, nameof(nonExistingAccount.Id), _context.Customers!);

        //Assert
        Assert.IsTrue(returnStatus == ItemExistStatus.ItemDoesNotExist);
    }
}