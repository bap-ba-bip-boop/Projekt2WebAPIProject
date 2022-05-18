using AutoMapper;
using WebAPI.DTO.TidsRegistrering;
using WebAPI.Model;

namespace WebAPI.Infrastructure.Profiles;

public class TidsRegistreringProfile : Profile
{
    public TidsRegistreringProfile()
    {
        CreateMap<TidsRegistrering, TidsRegistreringGetAllDTO>()
            .ForMember(
                src => src.ProjectName,
                opt => opt.MapFrom(
                    src => src.Project!.ProjectName
                )
            );
        CreateMap<TidsRegistrering, TidsRegistreringGetOneDTO>()
            .ForMember(
                src => src.ProjectName,
                opt => opt.MapFrom(
                    src => src.Project!.ProjectName
                )
            )
            .ForMember(
                src => src.CustomerName,
                opt => opt.MapFrom(
                    src => src.Project!.Customer!.Name
                )
            );
        CreateMap<TidsRegistreringPostDTO, TidsRegistrering>();
        CreateMap<TidsRegistreringPutDTO, TidsRegistrering>();
    }
}