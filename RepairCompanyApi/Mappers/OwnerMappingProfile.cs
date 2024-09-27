using AutoMapper;
using RepairCompanyApi.Dtos;
using RepairCompanyApi.Models;

namespace RepairCompanyApi.Mappers;

public class OwnerMappingProfile : Profile
{
    public OwnerMappingProfile()
    {
        CreateMap<PropertyOwner, OwnerDataDto>()
            .ForMember(
               dest => dest.OwnerId,
               opt => opt.MapFrom(src => src.Id))
            .ForMember(
               dest => dest.OwnerName,
               opt => opt.MapFrom(src =>
                   $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Buildings,
            opt => opt.MapFrom(src => src.BuildingProperties));

        CreateMap<BuildingProperty, BuildingOwnerDto>();
          //  .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.Id))  
         //   .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
    
            CreateMap<Repair, BuildingRepairDto>();  
    
    }
}
