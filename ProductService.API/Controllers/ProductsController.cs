namespace ProductService.API.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using MicroservicesPOC.Shared.Controllers;

    using ProductService.Application.Products.Queries;
    using ProductService.Application.Products.Commands;
    using ProductService.Application.Common.Models.Product;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpHead]
        [HttpGet(Name = "GetAllActiveProducts")]
        public async Task<ActionResult> Get() => 
            Ok(await this.Mediator.Send(new FindAllProductsQuery()));

        //[HttpGet("{code}", Name = "GetProductByCode")]
        //public async Task<ActionResult> GetByCode(string code) => 
            //Ok(await this.Mediator.Send(new FindProductByCodeQuery(code)));

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult> GetById(Guid id) => 
            Ok(await this.Mediator.Send(new FindProductByIdQuery(id)));

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductForCreationDTO model) => 
            CreatedAtRoute("GetProductById", new { id = await this.Mediator.Send(new CreateDraftProductCommand(model)) }, model);

        [HttpPost("{id}/activate")]
        public async Task<ActionResult> PostActivate(Guid id)
        {
            if (!await this.Mediator.Send(new ProductExistsQuery(id)))
                return NotFound();

            await this.Mediator.Send(new ActivateInsuranceCommand(id));

            return NoContent();
        }

        [HttpPost("{id}/discontinue")]
        public async Task<ActionResult> PostDiscontinue(Guid id)
        {
            if (!await this.Mediator.Send(new ProductExistsQuery(id)))
                return NotFound();

            await this.Mediator.Send(new DiscontinueInsuranceCommand(id));

            return NoContent();
        }
    }
}
