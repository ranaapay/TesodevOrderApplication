using AutoMapper;
using CustomerCore;
using CustomerCore.DataTransferObjects;

namespace CustomerApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(c => c.FullAdress,
                    opt => opt.MapFrom(x => string.Join(' ', x.Address.AddressLine, x.Address.Country, x.Address.City,x.Address.CityCode)));

            CreateMap<CustomerForCreationDto, Customer>();
            CreateMap<CustomerForUpdateDto, Customer>();
            CreateMap<AddressDto, Address>();
        }
    }
}