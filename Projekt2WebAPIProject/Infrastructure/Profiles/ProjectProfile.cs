using AutoMapper;
using WebAPI.DTO.Project;
using WebAPI.Model;

namespace WebAPI.Infrastructure.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectGetAllDTO>();
        CreateMap<Project, ProjectGetOneDTO>();
        CreateMap<ProjectPostDTO, Project>();
        CreateMap<ProjectPutDTO, Project>();
    }
}
