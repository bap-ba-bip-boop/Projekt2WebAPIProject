using AdministartionWebsite.ViewModels.Project;
using AutoMapper;
using SharedResources.Data;

namespace AdministartionWebsite.Infrastructure.Profiles;

public class TimeRegProfile : Profile
{
    public TimeRegProfile()
    {
        CreateMap<TidsRegistrering, ProjectPageViewModelListItem>()
            .ForMember(
                src => src.MinutesSpent,
                opt => opt.MapFrom(
                    src => src.AntalMinuter
                )
            )
            .ForMember(
                src => src.Date,
                opt => opt.MapFrom(
                    src => src.Datum
                )
            )
            .ForMember(
                src => src.Description,
                opt => opt.MapFrom(
                    src => (src.Beskrivning!.Length > 30)?
                    src.Beskrivning.Substring(0,27).Concat("..."):
                    src.Beskrivning
                )
            );
    }
}