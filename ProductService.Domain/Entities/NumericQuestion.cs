namespace ProductService.Domain.Entities
{
    public class NumericQuestion : Question
    {
        public NumericQuestion(string code, int index, string text): base(code, index, text) { }
    }
}
