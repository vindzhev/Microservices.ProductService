namespace ProductService.Application.Common.Models.Cover
{
    using System;

    public class CoverDTO
    {
        //public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Optional { get; set; }

        public Nullable<decimal> TotalInsured { get; set; }
    }
}
