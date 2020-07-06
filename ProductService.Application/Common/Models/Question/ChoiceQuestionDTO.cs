namespace ProductService.Application.Common.Models.Question
{
    using System.Collections.Generic;

    using ProductService.Application.Common.Models.Choice;

    public class ChoiceQuestionDTO : QuestionDTO
    {
        public ICollection<ChoiceDTO> Choices { get; set; }
    }
}
