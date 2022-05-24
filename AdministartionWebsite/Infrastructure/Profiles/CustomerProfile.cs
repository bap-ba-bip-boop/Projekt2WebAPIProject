using AdministartionWebsite.ViewModels.Customer;
using AutoMapper;
using SharedResources.Data;

namespace AdministartionWebsite.Infrastructure.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        var _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProjectProfile>();
        }
        ).CreateMapper();

        CreateMap<Customer, CustomerIndexVMListItem>();
        CreateMap<Customer, CustomerPageViewModel>()
            .ForMember(
                src => src.CustomerProjects,
                opt => opt.MapFrom(
                    src => src.Projects!
                        .Select(_mapper.Map<CustomerPageProjectListItem>)
                        .ToList()
                )
            );
        CreateMap<CustomerNewViewModel, Customer>();
        CreateMap<CustomerEditViewModel, Customer>()
            .ForMember(
                src => src.Projects,
                opt => opt.Ignore()
            )
            .ReverseMap();
    }
}