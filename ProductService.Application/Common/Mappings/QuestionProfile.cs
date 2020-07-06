namespace ProductService.Application.Common.Mappings
{
    using AutoMapper;

    using ProductService.Domain.Entities;
    using ProductService.Application.Common.Models.Question;

    using MicroservicesPOC.Shared.Common.Models;

    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            this.CreateMap<Question, QuestionDTO>()
                .IncludeAllDerived();

            this.CreateMap<NumericQuestion, NumericQuestionDTO>()
                .ForMember(x => x.Type, opt => opt.MapFrom(src => QuestionType.Numeric));

            this.CreateMap<DateQuestion, DateQuestionDTO>()
                .ForMember(x => x.Type, opt => opt.MapFrom(src => QuestionType.Text));

            this.CreateMap<ChoiceQuestion, ChoiceQuestionDTO>()
                .ForMember(x => x.Type, opt => opt.MapFrom(src => QuestionType.Choice));
        }
    }
}
