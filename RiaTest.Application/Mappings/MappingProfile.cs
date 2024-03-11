using AutoMapper;
using RiaTest.Application.DTOs;
using RiaTest.Domain.Entities;

namespace RiaTest.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InsertCustomerDTO, Customer>();
        }
    }
}
