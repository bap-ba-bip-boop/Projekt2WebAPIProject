using AdministartionWebsite.ViewModels.Customer;
using AutoMapper;
using SharedResources.Data;

namespace AdministartionWebsite.Infrastructure.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, CustomerPageProjectListItem>()
            .ForMember(
                src => src.regAmount,
                opt => opt.MapFrom(
                    src => src.TimeRegs!.Count()
                )
            )
            .ForMember(
                src => src.latestRegDate,
                opt => opt.MapFrom(
                    src => src.TimeRegs!.First().Datum
                )
            );
    }
}