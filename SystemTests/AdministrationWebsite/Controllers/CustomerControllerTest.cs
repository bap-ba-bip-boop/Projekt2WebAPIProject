using AdministartionWebsite.Controllers;
using AdministartionWebsite.Infrastructure.Profiles;
using AdministartionWebsite.ViewModels.Customer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedResources.Data;
using SharedResources.Services;
using System;
using System.Linq;
using SystemTests.Services;

namespace SystemTests.AdministrationWebsite.Controllers;

[TestClass]
public class CustomerControllerTest
{
    private readonly ApplicationDbContext _context;
    private readonly CustomerController _sut;
    private readonly IMapper _mapper;
    private readonly CreateUniqeService _creator;
    public CustomerControllerTest()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
        }
        ).CreateMapper();
        _creator = new CreateUniqeService();
        _context = TestDatabaseService.CreateTestContext(nameof(CustomerControllerTest));
        _sut = new CustomerController(_context,_mapper);
    }
    //Index
    [TestMethod]
    public void When_Call_CustomerIndex_Should_Return_Correct_ViewModel()
    {
        TestDatabaseService.AddCustomer(Guid.NewGuid().ToString(), _creator, _context);
        var result = _sut.CustomerIndex() as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(CustomerIndexViewModel));
    }
    //Page
    [TestMethod]
    public void When_Call_CustomerPage_Should_Return_Correct_ViewModel()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var result = _sut.CustomerPage(CustomerId) as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(CustomerPageViewModel));
    }
    //New
    [TestMethod]
    public void When_Call_CustomerNew_Should_Return_Correct_ViewModel()
    {

        var result = _sut.CustomerNew() as ViewResult;
        Assert.AreEqual(result!.Model, null);
    }
    [TestMethod]
    public void When_Call_CustomerNew_With_Correct_ViewModel_Should_Create_Customer()
    {
        var customerName = Guid.NewGuid().ToString();
        var customerVM = new CustomerNewViewModel
        {
            CustomerName = customerName
        };

        var result = _sut.CustomerNew(customerVM) as RedirectToActionResult;
        Assert.AreEqual(result!.ActionName, nameof(CustomerController.CustomerIndex));

        var addedCustomer = _context.Customers!.First(cust => cust.CustomerName == customerName);

        Assert.AreEqual(addedCustomer.CustomerName, customerName);
    }
    [TestMethod]
    public void When_Call_CustomerNew_With_Incorrect_ViewModel_Should_Return_ViewModel()
    {
        string customerName = null;
        var customerVM = new CustomerNewViewModel
        {
            CustomerName = customerName
        };
        _sut.ModelState.AddModelError("test", "test");
    
        var result = _sut.CustomerNew(customerVM) as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(CustomerNewViewModel));
    }
    //Edit
    [TestMethod]
    public void When_Call_CustomerEdit_With_Id_Should_Return_ViewModel()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var result = _sut.CustomerEdit(CustomerId) as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(CustomerEditViewModel));
    }
    [TestMethod]
    public void When_Call_CustomerEdit_With_Correct_ViewModel_Should_Modify_Customer()
    {
        var customerOldName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(customerOldName, _creator, _context);
        var customerToEdit = _context.Customers!.First(cust => cust.CustomerName == customerOldName);
        var CustomerId = customerToEdit.CustomerId;

        var customerNewName = Guid.NewGuid().ToString();
        var customerVM = new CustomerEditViewModel
        {
            CustomerName = customerNewName,
            CustomerId = CustomerId
        };

        var result = _sut.CustomerEdit(customerVM) as RedirectToActionResult;
        Assert.AreEqual(result!.ActionName, nameof(CustomerController.CustomerIndex));

        customerToEdit = _context.Customers!.First(cust => cust.CustomerId == CustomerId);

        Assert.AreEqual(customerToEdit.CustomerName, customerNewName);
    }
    [TestMethod]
    public void When_Call_CustomerEdit_With_Incorrect_ViewModel_Should_Return_ViewModel()
    {
        string customerName = null;
        var customerVM = new CustomerEditViewModel
        {
            CustomerName = customerName
        };
        _sut.ModelState.AddModelError("test", "test");

        var result = _sut.CustomerEdit(customerVM) as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(CustomerEditViewModel));
    }
    //Delete
    [TestMethod]
    public void When_Call_CustomerDelete_With_Id_Should_Be_Removed()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var result = _sut.CustomerDelete(CustomerId) as RedirectToActionResult;
        Assert.AreEqual(result!.ActionName, nameof(CustomerController.CustomerIndex));

        var deletedCustomer = _context.Customers!.FirstOrDefault(cust => cust.CustomerId == CustomerId);

        Assert.AreEqual(deletedCustomer, default);
    }

}