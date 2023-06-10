using BlazorShop.Domain.Commands.Categories;
using BlazorShop.Domain.Commands.Users;
using BlazorShop.Domain.Entities;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Handlers.Contracts;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Handlers.Categories
{
    public class CreateCategoryHandle : Notifiable<Notification>, IHandlerCommand<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandle(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ICommandResult Handler(CreateCategoryCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Correctly enter category data", command.Notifications);

            Category category = new Category(
                command.Name,
                command.IconCSS
            );

            if (!category.IsValid)
                return new GenericCommandResult(false, "Correctly enter category data", category.Notifications);

            _categoryRepository.Add(category);

            return new GenericCommandResult(true, "Category created successfully", category);
        }
    }
}
