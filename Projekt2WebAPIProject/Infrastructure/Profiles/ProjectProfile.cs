using AutoMapper;
using SharedResources.Data;
using WebAPI.DTO.Project;
using WebAPI.Model;

namespace WebAPI.Infrastructure.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectGetAllDTO>()
            .ForMember(
                src => src.CustomerName,
                opt => opt.MapFrom(
                        src => src.Customer!.Name
                    )
            );
        CreateMap<Project, ProjectGetOneDTO>()
            .ForMember(
                src => src.CustomerName,
                opt => opt.MapFrom(
                        src => src.Customer!.Name
                    )
            );
        CreateMap<ProjectPostDTO, Project>();
        CreateMap<ProjectPutDTO, Project>();
    }
}
