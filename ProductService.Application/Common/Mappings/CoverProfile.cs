namespace ProductService.Application.Common.Mappings
{
    using AutoMapper;

    using ProductService.Domain.Entities;
    using ProductService.Application.Common.Models.Cover;

    public class CoverProfile : Profile
    {
        public CoverProfile()
        {
            this.CreateMap<Cover, CoverDTO>();
        }
    }
}
