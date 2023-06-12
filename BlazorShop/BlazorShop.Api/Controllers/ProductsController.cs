using BlazorShop.Domain.Commands.Products;
using BlazorShop.Domain.Handlers.Products;
using BlazorShop.Domain.Queries.Products;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers
{
    [Route("v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Commands:

        // register a new product
        [Route("add")]
        [HttpPost]
        public GenericCommandResult AddProduct(CreateProductCommand command, [FromServices] CreateProductHandle handle)
        {
            return (GenericCommandResult)handle.Handler(command);
        }

        // delete a product
        [Route("delete/{id?}")]
        [HttpDelete]
        public GenericCommandResult Delete(Guid id, [FromServices] DeleteProductHandle handle)
        {
            var command = new DeleteProductCommand
            {
                Id = id
            };

            return (GenericCommandResult)handle.Handler(command);
        }



        // Queries:

        // list all products
        [Route("list")]
        [HttpGet]
        public GenericQueryResult List([FromServices] ListProductHandle handle)
        {
            ListProductQuery query = new ListProductQuery();

            return (GenericQueryResult)handle.Handler(query);
        }

        // search product by name
        [Route("searchName/{name}")]
        [HttpGet]
        public GenericQueryResult SearchByName(string name, [FromServices] SearchProductByNameHandle handle)
        {

            var query = new SearchProductByNameQuery
            {
                Name = name
            };

            return (GenericQueryResult)handle.Handler(query);
        }

        // search product by id
        [Route("searchId/{id}")]
        [HttpGet]
        public GenericQueryResult SearchById(Guid id, [FromServices] SearchProductByIdHandle handle)
        {
            var query = new SearchProductByIdQuery
            {
                Id = id
            };

            return (GenericQueryResult)handle.Handler(query);
        }
    }
}
