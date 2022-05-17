using AutoMapper;
using WebAPI.DTO.Customer;
using WebAPI.Model;

namespace WebAPI.Infrastructure.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerGetAllDTO>();
        CreateMap<Customer, CustomerGetOneDTO>();
        CreateMap<CustomerPostDTO, Customer>();
        CreateMap<CustomerPutDTO, Customer>();
    }
}
