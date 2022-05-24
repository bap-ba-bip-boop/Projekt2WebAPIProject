using AdministartionWebsite.Controllers;
using AdministartionWebsite.Infrastructure.Profiles;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Data;
using SystemTests.Services;

namespace SystemTests.AdministrationWebsite.Controllers;

[TestClass]
public class CustomerControllerTest
{
    private readonly ApplicationDbContext _context;
    private readonly CustomerController _sut;
    private readonly IMapper _mapper;
    public CustomerControllerTest()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
        }
        ).CreateMapper();
        _context = TestDatabaseService.CreateTestContext(nameof(CustomerControllerTest));
        _sut = new CustomerController(_context,_mapper);
    }
    [TestMethod]
    public void When_Call_CustomerIndex_Should_Return()
    {
        var result = _sut.CustomerIndex();
        Assert.IsTrue(false);
    }
}