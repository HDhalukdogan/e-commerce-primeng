using AutoMapper;
using MediatRApi.Application.Common.Models;
using MediatRApi.Application.Common.Responses;
using MediatRApi.Entities;

namespace MediatRApi.Application.Common.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<DemoEntity, DemoEntityResponse>();
    CreateMap<Category, CategoryResponse>();
    CreateMap<Product, ProductResponse>();
    CreateMap<Product, HomeProduct>()
      .ForMember(dest=>dest.CoverImage, opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault(s=>s.IsCover).Image));
    CreateMap<ProductImage, ProductImageResponse>();
  }
}
