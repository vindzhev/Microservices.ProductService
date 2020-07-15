namespace ProductService.Application.Common.Models.Question
{
    using System.Collections.Generic;

    using MicroservicesPOC.Shared.Domain.Enums;
    using ProductService.Application.Common.Models.Choice;

    public class QuestionorCreationDTO
    {
        public string Code { get; set; }

        public int Index { get; set; }

        public string Text { get; set; }

        public QuestionType Type { get; set; }

        public ICollection<ChoiceDTO> Choices { get; set; }
    }
}
