using AutoMapper;
using Truck.Registration.Application.UseCases.Get;
using Truck.Registration.Application.UseCases.GetById;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.Application.Profiles
{
    public class GetProfile : Profile
    {
        public GetProfile()
        {
            CreateMap<GetResponse, Entity.Truck>().ReverseMap();
            CreateMap<GetByIdResponse, Entity.Truck>().ReverseMap();
        }
    }
}