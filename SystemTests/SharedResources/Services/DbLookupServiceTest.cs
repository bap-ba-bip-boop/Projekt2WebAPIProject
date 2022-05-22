using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Data;
using SharedResources.Services;
using System;
using System.Linq;
using SystemTests.Services;
using static SharedResources.Services.IDbLookupService;

namespace SystemTests.Inlämning_API.Services;

[TestClass]
public class DbLookupServiceTest
{

    private readonly ApplicationDbContext _context;
    private readonly DbLookupService _sut;
    private readonly ICreateUniqeService _creator;
    public DbLookupServiceTest()
    {
        _context = TestDatabaseService.CreateTestContext(nameof(DbLookupServiceTest));
        _creator = new CreateUniqeService();

        addItem(Guid.NewGuid().ToString());

        _sut = new DbLookupService();
    }
    private void addItem(string name) =>
        _creator.CreateIfNotExists(
            _context,
            _context.Customers!,
            item => item.Name!.Equals(name),
            new Customer
            {
                Name = name
            }
        );

    [TestMethod]
    public void When_Account_Exists_Should_Return_AdExists()
    {
        var existingItem = _context.Customers!.First();

        var (returnStatus, returnCustomer) = _sut.VerifyItemID(existingItem.CustomerId, nameof(existingItem.CustomerId), _context.Customers!.ToList());

        Assert.IsTrue(returnStatus == ItemExistStatus.ItemExists);
        Assert.IsFalse(returnCustomer == null);
    }
    [TestMethod]
    public void When_Account_Dont_Exist_Should_Return_AdDoesNotExist()
    {
        var nonExistingAccount = new Customer
        {
            CustomerId = -1,
            Name = "name"
        };

        var (returnStatus, returnAd) = _sut.VerifyItemID(nonExistingAccount.CustomerId, nameof(nonExistingAccount.CustomerId), _context.Customers!.ToList());

        Assert.IsTrue(returnStatus == ItemExistStatus.ItemDoesNotExist);
    }
}