using BlazorShop.Domain.Commands.Categories;
using BlazorShop.Domain.Commands.ItemsCart;
using BlazorShop.Domain.Handlers.Categories;
using BlazorShop.Domain.Handlers.ItemsCart;
using BlazorShop.Domain.Queries.Categories;
using BlazorShop.Domain.Queries.ItemsCart;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlazorShop.Api.Controllers
{
    [Route("v1/itemsCart")]
    [ApiController]
    public class ItemsCartController : ControllerBase
    {
        // Commands:

        // register a new item
        [Route("add")]
        [HttpPost]
        public GenericCommandResult AddItem(CreateItemCartCommand command, [FromServices] CreateItemCartHandle handle)
        {
            return (GenericCommandResult)handle.Handler(command);
        }

        // delete a item
        [Route("delete/{id?}")]
        [HttpDelete]
        public GenericCommandResult Delete(Guid id, [FromServices] DeleteItemCartHandle handle)
        {
            var command = new DeleteItemCartCommand
            {
                Id = id
            };

            return (GenericCommandResult)handle.Handler(command);
        }



        // Queries:

        // list all items
        [Route("list")]
        [HttpGet]
        public GenericQueryResult List([FromServices] ListItemCartHandle handle)
        {
            ListItemCartQuery query = new ListItemCartQuery();

            return (GenericQueryResult)handle.Handler(query);
        }

        // search item by id
        [Route("searchId/{id}")]
        [HttpGet]
        public GenericQueryResult SearchById(Guid id, [FromServices] SearchItemCartByIdHandle handle)
        {
            var query = new SearchItemCartByIdQuery
            {
                Id = id
            };

            return (GenericQueryResult)handle.Handler(query);
        }
    }
}
