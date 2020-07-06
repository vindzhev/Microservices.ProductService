namespace ProductService.Application.Common.Models.Product
{
    using System.Collections.Generic;

    using ProductService.Application.Common.Models.Cover;
    using ProductService.Application.Common.Models.Question;

    public class ProductForCreationDTO
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int InsuredLimit { get; set; }

        public ICollection<CoverForCreationDTO> Covers { get; set; }

        public ICollection<QuestionorCreationDTO> Questions { get; set; }
    }
}
