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
                    src => (src.TimeRegs!.Count() > 0) ? src.TimeRegs!.Last().Datum.ToString() : "Never"
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
                    .OrderByDescending( src => src.Date)
                    .ToList()
                )
            )
            ;
        CreateMap<ProjectNewViewModel, Project>();
        CreateMap<Project, ProjectEditViewModel>()
            .ReverseMap();
            
    }
}