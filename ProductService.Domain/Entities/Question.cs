namespace ProductService.Domain.Entities
{
    using System;

    using MicroservicesPOC.Shared.Domain;

    public class Question : Entity<Guid>
    {
        public Question() { }

        protected Question(string code, int index, string text)
        {
            this.Id = Guid.NewGuid();
            this.Code = code;
            this.Index = index;
            this.Text = text;
        }

        public string Code { get; private set; }

        public int Index { get; private set; }

        public string Text { get; private set; }

        public Product Product { get; protected set; }
    }
}