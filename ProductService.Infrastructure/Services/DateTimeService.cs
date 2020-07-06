namespace ProductService.Infrastructure.Services
{
    using System;

    using MicroservicesPOC.Shared.Common.Interfaces;


    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
