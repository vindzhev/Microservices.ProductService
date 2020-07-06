namespace ProductService.API.Services
{
    using System;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Http;
    
    using MicroservicesPOC.Shared.Common.Interfaces;

    public class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
            this.UserId = string.IsNullOrEmpty(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)) ?
                Guid.Parse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)) : Guid.NewGuid();

        public Guid UserId { get; }
    }
}
