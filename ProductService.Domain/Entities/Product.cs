namespace ProductService.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using MicroservicesPOC.Shared.Domain;

    public class Product : Entity<Guid>
    {
        public Product() { }

        public Product(string code, string name, string image, string description, int limit)
        {
            this.Id = Guid.NewGuid();
            this.Code = code;
            this.Name = name;
            this.Status = ProductStatus.Draft;
            this.Image = image;
            this.Description = description;
            this.InsuredLimit = limit;

            this.Covers = new List<Cover>();
            this.Questions = new List<Question>();
        }

        public string Code { get; private set; }

        public string Name { get; private set; }

        public ProductStatus Status { get; private set; }

        public string Image { get; private set; }

        public string Description { get; private set; }

        public int InsuredLimit { get; private set; }

        public ICollection<Cover> Covers { get; private set; }

        public ICollection<Question> Questions { get; private set; }

        public static Product CreateDraft(string code, string name, string image, string description, int limit) => new Product(code, name, image, description, limit);

        private void EnsureInDraftState()
        {
            if (this.Status != ProductStatus.Draft)
                throw new ApplicationException("Only draft version can be modified and activated");
        }

        public void Activate()
        {
            EnsureInDraftState();
            this.Status = ProductStatus.Active;
        }

        public void Discontinue() => this.Status = ProductStatus.Discontinued;

        public void AddCover(string code, string name, string description, bool optional, Nullable<decimal> totalInsured)
        {
            EnsureInDraftState();
            this.Covers.Add(new Cover(code, name, description, optional, totalInsured));
        }

        public void AddQuestions(IEnumerable<Question> questions)
        {
            EnsureInDraftState();
            foreach (var question in questions)
                this.Questions.Add(question);
        }
    }
}
