using AdministartionWebsite.ViewModels.Customer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;

namespace AdministartionWebsite.Controllers;

public class CustomerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public IActionResult CustomerIndex()
    {
        var ciViewModel = new CustomerIndexViewModel
        {
            Customers = _context.Customers!
                .Select(_mapper.Map<CustomerIndexVMListItem>)
                .ToList()
        };
        return View(ciViewModel);
    }
    public IActionResult CustomerPage(int Id)
    {
        var customer = _context.Customers!
        .Include(cust => cust.Projects)!
        .ThenInclude(proj => proj.TimeRegs)
        .First(cust => cust.CustomerId == Id);

        var cpViewModel = _mapper.Map<CustomerPageViewModel>(customer);

        return View(cpViewModel);
    }
    [HttpGet]
    public IActionResult CustomerNew()
    {
        return View();
    }
    [HttpPost]
    public IActionResult CustomerNew(CustomerNewViewModel cnViewModel)
    {
        if(ModelState.IsValid)
        {
            _context.Customers!.Add(
                _mapper.Map<Customer>(cnViewModel)
            );
            _context.SaveChanges();
            return RedirectToAction(nameof(CustomerIndex));
        }
        return View(cnViewModel);
    }
    [HttpGet]
    public IActionResult CustomerEdit(int Id)
    {
        var customerToEdit = _context.Customers!.First(cust => cust.CustomerId == Id);

        var editViewModel = _mapper.Map<CustomerEditViewModel>(customerToEdit);

        return View(editViewModel);
    }
    [HttpPost]
    public IActionResult CustomerEdit(CustomerEditViewModel ceViewModel)
    {
        if(ModelState.IsValid)
        {
            var customerToEdit = _context.Customers!.First(cust => cust.CustomerId == ceViewModel.CustomerId);

            _mapper.Map(ceViewModel, customerToEdit);
            _context.SaveChanges();

            return RedirectToAction(nameof(CustomerIndex));
        }
        return View(ceViewModel);
    }
    public IActionResult CustomerDelete(int Id)
    {
        var customerToDelete = _context.Customers!.First(cust => cust.CustomerId == Id);

        _context.Customers!.Remove(customerToDelete);
        _context.SaveChanges();

        return RedirectToAction(nameof(CustomerIndex));
    }
}
