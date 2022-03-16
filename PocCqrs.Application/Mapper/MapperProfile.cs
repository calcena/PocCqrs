

namespace PocCqrs.Application.Mapper
{
    using AutoMapper;
    using PocCqrs.Application.Models;
    using PocCqrs.Domain;
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            DtoToEntity();
            EntityToDto();
        }

        private void EntityToDto()
        {
            CreateMap<ProductDto, Product>()
                .ForMember(dest =>
                dest.Id,
                opt => opt.Ignore()
                )
                .ForMember(dest =>
                dest.Name,
                opt => opt.Ignore()
                )
                .ForMember(dest =>
                dest.Description,
                opt => opt.Ignore()
                )
                .ForMember(dest =>
                dest.CreatedAt,
                opt => opt.Ignore()
                )
                .ForMember(dest =>
                dest.ModifiedAt,
                opt => opt.Ignore()
                )
                .ForMember(dest =>
                dest.Audit,
                opt => opt.Ignore()
                );
        }
        private void DtoToEntity()
        {
            CreateMap<Product, ProductDto>();
        }
    }

}
