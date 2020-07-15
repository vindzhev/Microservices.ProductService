namespace ProductService.Application.Common.Models.Question
{
    using System;
   
    using MicroservicesPOC.Shared.Domain.Enums;

    public class QuestionDTO
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public int Index { get; set; }

        public string Text { get; set; }

        public QuestionType Type { get; set; }
    }
}
