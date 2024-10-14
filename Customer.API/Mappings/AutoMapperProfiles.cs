using AutoMapper;
using Customer.API.Models.Domain;
using Customer.API.Models.DTO;

namespace Customer.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        //// Mapper profiles to convert Domain Objects to Data Transfer Objects and vice versa
        public AutoMapperProfiles()
        {            
            CreateMap<CustomerDomain, CustomerDto>().ReverseMap();

            CreateMap<AddOrUpdateCustomerDto, CustomerDomain>().ReverseMap();
        }
    }
}
