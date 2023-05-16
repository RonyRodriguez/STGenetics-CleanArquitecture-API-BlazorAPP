using AutoMapper;
using STG.Domain.Domain;
using STG.Domain.Entity;

namespace STG.Service.Profiles
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<Animal, AnimalDomain>();
            CreateMap<AnimalDomain, Animal>();

        }
    }
}
