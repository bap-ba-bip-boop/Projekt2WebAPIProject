using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Data;
using SharedResources.Services;
using System;
using SystemTests.Services;
using static SharedResources.Services.ICreateUniqeService;

namespace SystemTests.SharedResources;

[TestClass]
public class CreateUniqueServiceTest
{
    public string TestName { get; set; }

    private readonly ApplicationDbContext _context;

    private readonly ICreateUniqeService _sut;
    public CreateUniqueServiceTest()
    {
        _context = TestDatabaseService.CreateTestContext(nameof(CreateUniqueServiceTest));
        _sut = new CreateUniqeService();

        TestName = Guid.NewGuid().ToString();

        _ = addCustomer(TestName);

    }
    private CreateUniqueStatus addCustomer(string name) =>
        _sut.CreateIfNotExists(
            _context,
            _context.Customers!,
            item => item.CustomerName!.Equals(name),
            new Customer
            {
                CustomerName = name,
            }
        );
    [TestMethod]
    public void When_Ad_Exists_Should_Return_AlreadyExists()
    {
        var name = TestName;

        var returnStatus = addCustomer(name);

        Assert.IsTrue(returnStatus == CreateUniqueStatus.AlreadyExists);
    }
    [TestMethod]
    public void When_Ad_Dont_Exists_Should_Return_Ok()
    {
        var title = Guid.NewGuid().ToString();

        var returnStatus = addCustomer(title);

        Assert.IsTrue(returnStatus == CreateUniqueStatus.Ok);
    }
}