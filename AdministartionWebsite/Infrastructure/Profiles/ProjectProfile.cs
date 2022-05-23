using AdministartionWebsite.ViewModels.Customer;
using AdministartionWebsite.ViewModels.Project;
using AutoMapper;
using SharedResources.Data;

namespace AdministartionWebsite.Infrastructure.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        var _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TimeRegProfile>();
        }

        ).CreateMapper();
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
        CreateMap<Project, ProjectIndexVMListItem>()
            .ForMember(
                src => src.CustomerName,
                opt => opt.MapFrom(
                    src => src.Customer!.CustomerName
                )
            )
            ;
        CreateMap<Project, ProjectPageViewModel>()
            .ForMember(
                src => src.CustomerName,
                opt => opt.MapFrom(
                    src => src.Customer!.CustomerName
                )
            )
            .ForMember(
                src => src.TotalMinutesSpent,
                opt => opt.MapFrom(
                    src => src.TimeRegs!
                    .Select(reg =>
                        reg.AntalMinuter
                    )
                    .Sum()
                )
            )
            .ForMember(
                src => src.TimeRegs,
                opt => opt.MapFrom(
                    src => src.TimeRegs!
                    .Select(_mapper.Map<ProjectPageViewModelListItem>)
                    .ToList()
                )
            )
            ;
        CreateMap<ProjectNewViewModel, Project>();
        CreateMap<Project, ProjectEditViewModel>()
            .ReverseMap();
            
    }
}