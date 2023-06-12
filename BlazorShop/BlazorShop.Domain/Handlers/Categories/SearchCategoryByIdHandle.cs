using BlazorShop.Domain.Interfaces;
using BlazorShop.Domain.Queries.Categories;
using BlazorShop.Domain.Queries.Users;
using BlazorShop.Shared.Commands;
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
    public class SearchCategoryByIdHandle : Notifiable<Notification>, IHandlerQuery<SearchCategoryByIdQuery>
    {
        private readonly ICategoryRepository _categoryRepository;

        public SearchCategoryByIdHandle(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IQueryResult Handler(SearchCategoryByIdQuery query)
        {
            query.Validate();

            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Correctly enter category data", query.Notifications);
            }

            var searchedCategory = _categoryRepository.SearchById(query.Id);

            if (searchedCategory == null)
            {
                return new GenericQueryResult(false, "Category not found", query.Notifications);
            }

            return new GenericQueryResult(true, "Categories found!", searchedCategory);
        }
    }
}
