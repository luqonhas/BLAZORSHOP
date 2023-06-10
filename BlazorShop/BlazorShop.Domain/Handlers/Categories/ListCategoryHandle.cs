using BlazorShop.Domain.Interfaces;
using BlazorShop.Domain.Queries.Categories;
using BlazorShop.Domain.Queries.Users;
using BlazorShop.Shared.Handlers.Contracts;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.Categories
{
    public class ListCategoryHandle : Notifiable<Notification>, IHandlerQuery<ListCategoryQuery>
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListCategoryHandle(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IQueryResult Handler(ListCategoryQuery query)
        {
            var list = _categoryRepository.List();

            return new GenericQueryResult(true, "Categories found!", list);
        }
    }
}
