using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Services;
using WebAPI.DTO.Customer;
using WebAPI.Model;
using WebAPI.Services;
using static SharedResources.Services.IDbLookupService;

namespace WebAPI.Controllers;

[ApiController]
[Route("customer")]
public class CusotmerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly APIDbContext _context;
    private readonly IDbLookupService _lookup;
    private readonly IAPIMethodWrapperService _methodWrapepr;

    public CusotmerController(IMapper mapper, APIDbContext context, IDbLookupService lookup, IAPIMethodWrapperService amws)
    {
        _mapper = mapper;
        _context = context;
        _lookup = lookup;
        _methodWrapepr = amws;
    }
    [HttpGet]
    public IActionResult GetAllCustomers() =>
        Ok(
            _context.Customers!.Select(customer =>
               _mapper.Map<CustomerGetAllDTO>(customer)
            )
        );
    [HttpGet]
    [Route("{Id}")]
    public IActionResult GetCustomerById(int Id)
    {
        var (status, customer) = _lookup.VerifyItemID(Id, nameof(Customer.CustomerId), _context.Customers!.ToList());
        return (status == ItemExistStatus.ItemDoesNotExist) ?
            NotFound() :
            Ok(
                _mapper.Map<CustomerGetOneDTO>(customer)
            );
    }
    [HttpPost]
    public IActionResult AddNewCustomer(CustomerPostDTO cpd)
    {
        var newCustomer = _mapper.Map<Customer>(cpd);

        _context.Customers!.Add(newCustomer);
        _context.SaveChanges();

        return CreatedAtAction(
            nameof(GetCustomerById),
            new { Id = newCustomer.CustomerId },
            _mapper.Map<CustomerGetOneDTO>(newCustomer)
        );
    }
    [HttpPut]
    [Route("{Id}")]
    public IActionResult ReplaceCustomerByID(int Id, CustomerPutDTO cpd)
    {
        var (status, customerToEdit) = _lookup.VerifyItemID(Id, nameof(Customer.CustomerId), _context.Customers!.ToList());
        return (status == ItemExistStatus.ItemDoesNotExist) ?
            NotFound() :
            _methodWrapepr.NonSafeHTTPMEthodWrapper(
                () => _mapper.Map(cpd, customerToEdit)
            );
    }
    [HttpDelete]
    [Route("{Id}")]
    public IActionResult DeleteCustomerByID(int Id)
    {
        var (status, customerToRemove) = _lookup.VerifyItemID(Id, nameof(Customer.CustomerId), _context.Customers!.ToList());
        return (status.Equals(ItemExistStatus.ItemDoesNotExist)) ?
            NotFound() :
            _methodWrapepr.NonSafeHTTPMEthodWrapper(
                () => _context.Customers!.Remove(customerToRemove)
            );
    }
    [HttpPatch]
    [Route("{Id}")]
    public IActionResult UpdateCustomerPropertyByID(int Id, [FromBody] JsonPatchDocument<Customer> customerEntity)
    {
        var (status, customerToPatch) = _lookup.VerifyItemID(Id, nameof(Customer.CustomerId), _context.Customers!.ToList());
        return (status == ItemExistStatus.ItemDoesNotExist) ?
            NotFound() :
            _methodWrapepr.NonSafeHTTPMEthodWrapper(
                () => customerEntity.ApplyTo(customerToPatch)
            );
    }
}