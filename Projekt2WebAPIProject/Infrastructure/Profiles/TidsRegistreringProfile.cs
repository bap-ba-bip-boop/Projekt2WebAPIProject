using AutoMapper;
using SharedResources.Data;
using WebAPI.DTO.TidsRegistrering;

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
                    src => src.Project!.Customer!.CustomerName
                )
            );
        CreateMap<TidsRegistreringPostDTO, TidsRegistrering>();
        CreateMap<TidsRegistreringPutDTO, TidsRegistrering>();
    }
}