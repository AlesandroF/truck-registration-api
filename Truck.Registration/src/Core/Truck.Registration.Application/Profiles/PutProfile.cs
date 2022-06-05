using AutoMapper;
using Truck.Registration.Application.UseCases.Put;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.Application.Profiles
{
    public class PutProfile : Profile
    {
        public PutProfile()
        {
            CreateMap<PutCommand, Entity.Truck>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(e => DateTime.UtcNow))
                .ReverseMap(); 

            CreateMap<PutResponse, Entity.Truck>().ReverseMap(); 
        }
    }
}