namespace ProductService.Application.Common.Models.Product
{
    using System;
    using System.Collections.Generic;
    
    using ProductService.Application.Common.Models.Cover;
    using ProductService.Application.Common.Models.Question;

    public class ProductDTO
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public ProductStatusDTO Status { get; set; }

        public ICollection<CoverDTO> Covers { get; set; }

        public ICollection<QuestionDTO> Questions { get; set; }

        public int InsuredLimit { get; set; }
    }

    public enum ProductStatusDTO
    {
        Draft,
        Active,
        Discontinued
    }
}
