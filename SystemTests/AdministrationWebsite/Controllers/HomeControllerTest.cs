using AdministartionWebsite.Controllers;
using AdministartionWebsite.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Data;
using SharedResources.Services;
using System;
using System.Linq;
using SystemTests.Services;

namespace SystemTests.AdministrationWebsite.Controllers;

[TestClass]
public class HomeControllerTest
{
    private readonly ApplicationDbContext _context;
    private readonly HomeController _sut;
    private readonly CreateUniqeService _creator;

    public HomeControllerTest()
    {
        _creator = new CreateUniqeService();
        _context = TestDatabaseService.CreateTestContext(nameof(HomeControllerTest));
        _sut = new HomeController(_context);
    }

    [TestMethod]
    public void When_Call_Home_Index_Should_Return_ViewModel()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var ProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(ProjectName, CustomerId, _creator, _context);

        var result = _sut.Index() as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(HomeIndexViewModel));
    }
}
