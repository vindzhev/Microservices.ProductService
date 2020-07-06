namespace ProductService.Domain.Entities
{
    using System.Collections.Generic;

    public class ChoiceQuestion : Question
    {
        public ChoiceQuestion() { }

        public ChoiceQuestion(string code, int index, string text, ICollection<Choice> choices) : 
            base(code, index, text) => this.Choices = choices;

        public ICollection<Choice> Choices { get; set; }

        public static ICollection<Choice> YesNoChoice()
            => new List<Choice>() { new Choice("YES", "Yes"), new Choice("NO", "No") };
    }
}
