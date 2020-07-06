namespace ProductService.Application.Common.Mappings
{
    using AutoMapper;

    using ProductService.Domain.Entities;
    using ProductService.Application.Common.Models.Choice;

    public class ChoiceProfile : Profile
    {
        public ChoiceProfile()
        {
            this.CreateMap<Choice, ChoiceDTO>();
            this.CreateMap<ChoiceDTO, Choice>();
        }
    }
}
