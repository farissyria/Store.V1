using AutoMapper;
using Store.Application.DTOs;
using Store.Core.Entities;
using static Store.Application.DTOs.AuthDto;

namespace Store.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ✅ Ignore Id when mapping from DTO to Entity
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id (DB will generate)
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            // For update, ignore Id as well
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            // Map Entity to DTO (include Id)
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));

            // User mappings (for Auth responses)
            CreateMap<User, AuthResponseDto>()
                .ForMember(dest => dest.Token, opt => opt.Ignore()) // Token is set separately
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

        }
    }
}