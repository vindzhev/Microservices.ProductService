namespace ProductService.Domain.Entities
{
    using System;

    using MicroservicesPOC.Shared.Domain;

    public class Cover : Entity<Guid>
    {
        public Cover() { }

        public Cover(string code, string name, string description, bool optional, Nullable<decimal> totalInsured)
        {
            this.Id = Guid.NewGuid();
            this.Code = code;
            this.Name = name;
            this.Description = description;
            this.Optional = optional;
            this.TotalInsured = totalInsured;
        }

        public string Code { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool Optional { get; private set; }

        public Nullable<decimal> TotalInsured { get; private set; }

        public Product Product { get; private set; }
    }
}