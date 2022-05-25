using AdministartionWebsite.Controllers;
using AdministartionWebsite.Infrastructure.Profiles;
using AdministartionWebsite.ViewModels.Project;
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
public class ProjectControllerTest
{
    private readonly ApplicationDbContext _context;
    private readonly ProjectController _sut;
    private readonly IMapper _mapper;
    private readonly CreateUniqeService _creator;
    public ProjectControllerTest()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProjectProfile>();
        }
        ).CreateMapper();
        _creator = new CreateUniqeService();
        _context = TestDatabaseService.CreateTestContext(nameof(ProjectControllerTest));
        _sut = new ProjectController(_context, _mapper);
    }

    //Index
    [TestMethod]
    public void When_Call_ProjectIndex_Should_Return_Correct_ViewModel()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var ProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(ProjectName, CustomerId, _creator, _context);

        var result = _sut.ProjectIndex() as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(ProjectIndexViewModel));
    }
    //Page
    [TestMethod]
    public void When_Call_ProjectPage_Should_Return_Correct_ViewModel()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var ProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(ProjectName, CustomerId, _creator, _context);
        var ProjectId = _context.Projects!.First(proj => proj.ProjectName == ProjectName).ProjectId;

        var result = _sut.ProjectPage(ProjectId) as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(ProjectPageViewModel));
    }
    //New
    [TestMethod]
    public void When_Call_ProjectNew_Should_Return_Correct_ViewModel()
    {
        var result = _sut.ProjectNew() as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(ProjectNewViewModel));
    }
    [TestMethod]
    public void When_Call_ProjectNew_With_Correct_View_Model_Should_Create_Project()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var ProjectName = Guid.NewGuid().ToString();
        var ProjectVM = new ProjectNewViewModel
        {
            ProjectName = ProjectName,
            CustomerId = CustomerId
        };

        var result = _sut.ProjectNew(ProjectVM) as RedirectToActionResult;
        Assert.AreEqual(result!.ActionName, nameof(ProjectController.ProjectIndex));

        var addedProject = _context.Projects!.First(proj => proj.ProjectName == ProjectName);

        Assert.AreEqual(addedProject.ProjectName, ProjectName);
        Assert.AreEqual(addedProject.CustomerId, CustomerId);
    }
    [TestMethod]
    public void When_Call_ProjectNew_With_Incorrect_ViewModel_Should_Return_ViewModel()
    {
        var ProjectVM = new ProjectNewViewModel
        {
            ProjectName = "",
            CustomerId = -1
        };
        _sut.ModelState.AddModelError("test", "test");

        var result = _sut.ProjectNew(ProjectVM) as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(ProjectNewViewModel));
    }
    //Edit
    [TestMethod]
    public void When_Call_ProjectEdit_With_Id_Should_Return_View_Model()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var ProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(ProjectName, CustomerId, _creator, _context);
        var ProjectId = _context.Projects!.First(proj => proj.ProjectName == ProjectName).ProjectId;

        var result = _sut.ProjectEdit(ProjectId) as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(ProjectEditViewModel));
    }
    [TestMethod]
    public void When_Call_ProjectEdit_With_Correct_View_Model_Should_Modify_Project()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var ProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(ProjectName, CustomerId, _creator, _context);
        var ProjectId = _context.Projects!.First(proj => proj.ProjectName == ProjectName).ProjectId;

        var ProjectVM = new ProjectEditViewModel
        {
            ProjectName = ProjectName,
            ProjectId = ProjectId
        };

        var result = _sut.ProjectEdit(ProjectVM) as RedirectToActionResult;
        Assert.AreEqual(result!.ActionName, nameof(ProjectController.ProjectIndex));

        var editedProject = _context.Projects!.First(proj => proj.ProjectId == ProjectId);

        Assert.AreEqual(editedProject.ProjectName, ProjectName);
    }
    [TestMethod]
    public void When_Call_ProjectEdit_With_Incorrect_ViewModel_Should_Return_ViewModel()
    {
        var ProjectVM = new ProjectEditViewModel
        {
            ProjectName = "",
            ProjectId = -1
        };
        _sut.ModelState.AddModelError("test", "test");

        var result = _sut.ProjectEdit(ProjectVM) as ViewResult;
        Assert.AreEqual(result!.Model!.GetType().Name, nameof(ProjectEditViewModel));
    }
    //Delete
    [TestMethod]
    public void When_Call_ProjectDelete_With_Id_Should_Be_Removed()
    {
        var CustomerName = Guid.NewGuid().ToString();
        TestDatabaseService.AddCustomer(CustomerName, _creator, _context);
        var CustomerId = _context.Customers!.First(cust => cust.CustomerName == CustomerName).CustomerId;

        var ProjectName = Guid.NewGuid().ToString();
        TestDatabaseService.AddProject(ProjectName, CustomerId, _creator, _context);
        var ProjectId = _context.Projects!.First(proj => proj.ProjectName == ProjectName).ProjectId;

        var result = _sut.ProjectDelete(ProjectId) as RedirectToActionResult;
        Assert.AreEqual(result!.ActionName, nameof(ProjectController.ProjectIndex));

        var deletedProject = _context.Projects!.FirstOrDefault(proj => proj.ProjectId == ProjectId);

        Assert.AreEqual(deletedProject, default);
    }
}
