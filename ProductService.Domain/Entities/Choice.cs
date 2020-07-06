namespace ProductService.Domain.Entities
{
    using System;

    using MicroservicesPOC.Shared.Common.Entities;

    public class Choice : Entity<Guid>
    {
        public Choice() { }

        public Choice(string code, string label)
        {
            this.Code = code;
            this.Label = label;
        }

        public string Code { get; private set; }

        public string Label { get; private set; }

        public ChoiceQuestion Question { get; private set; }
    }
}