using AutoMapper;
using twoday_Internship_Task.Database.DatabaseModels;
using twoday_Internship_Task.DtoModels;
using twoday_Internship_Task.Models;

namespace twoday_Internship_Task.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AnimalModel, Animal>();
            CreateMap<Animal, AnimalModel>();
            CreateMap<EnclosureModel, Enclosure>();
            CreateMap<string, EnclosureObject>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));
            CreateMap<EnclosureObject, string>()
                .ConvertUsing(source => source.Name ?? string.Empty);
            CreateMap<Enclosure, EnclosureGETModel>();
        }
    }
}
