using BlazorShop.Domain.Commands.Categories;
using BlazorShop.Domain.Commands.Users;
using BlazorShop.Domain.Handlers.Categories;
using BlazorShop.Domain.Handlers.Users;
using BlazorShop.Domain.Queries.Categories;
using BlazorShop.Domain.Queries.Users;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlazorShop.Api.Controllers
{
    [Route("v1/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Commands:

        // register a new category
        [Route("add")]
        [HttpPost]
        public GenericCommandResult AddCategory(CreateCategoryCommand command, [FromServices] CreateCategoryHandle handle)
        {
            return (GenericCommandResult)handle.Handler(command);
        }

        // delete a category
        [Route("delete/{id?}")]
        [HttpDelete]
        public GenericCommandResult Delete(Guid id, [FromServices] DeleteCategoryHandle handle)
        {
            var command = new DeleteCategoryCommand
            {
                Id = id
            };

            return (GenericCommandResult)handle.Handler(command);
        }



        // Queries:

        // list all categories
        [Route("list")]
        [HttpGet]
        public GenericQueryResult List([FromServices] ListCategoryHandle handle)
        {
            ListCategoryQuery query = new ListCategoryQuery();

            return (GenericQueryResult)handle.Handler(query);
        }

        // search category by id
        [Route("searchId/{id}")]
        [HttpGet]
        public GenericQueryResult SearchById(Guid id, [FromServices] SearchCategoryByIdHandle handle)
        {
            var query = new SearchCategoryByIdQuery
            {
                Id = id
            };

            return (GenericQueryResult)handle.Handler(query);
        }
    }
}
