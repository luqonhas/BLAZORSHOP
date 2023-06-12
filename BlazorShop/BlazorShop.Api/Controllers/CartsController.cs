using BlazorShop.Domain.Commands.Carts;
using BlazorShop.Domain.Commands.Products;
using BlazorShop.Domain.Handlers.Carts;
using BlazorShop.Domain.Handlers.Products;
using BlazorShop.Domain.Queries.Carts;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers
{
    [Route("v1/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        // Queries

        // list all carts
        [Route("list")]
        [HttpGet]
        public GenericQueryResult List([FromServices] ListCartHandle handle)
        {
            ListCartQuery query = new ListCartQuery();

            return (GenericQueryResult)handle.Handler(query);
        }
    }
}
