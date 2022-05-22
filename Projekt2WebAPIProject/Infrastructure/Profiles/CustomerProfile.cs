using AutoMapper;
using SharedResources.Data;
using WebAPI.DTO.Customer;

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
