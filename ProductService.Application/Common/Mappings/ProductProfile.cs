namespace ProductService.Application.Common.Mappings
{
    using AutoMapper;

    using ProductService.Domain.Entities;
    using ProductService.Application.Common.Models.Product;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Product, ProductDTO>();
        }
    }
}
