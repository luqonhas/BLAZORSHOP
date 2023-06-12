using BlazorShop.Domain.Commands.Products;
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

namespace BlazorShop.Domain.Handlers.Products
{
    public class CreateProductHandle : Notifiable<Notification>, IHandlerCommand<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateProductHandle(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public ICommandResult Handler(CreateProductCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Correctly enter product data", command.Notifications);

            var category = _categoryRepository.SearchById(command.IdCategory);

            if (category == null)
                return new GenericCommandResult(false, "Invalid category", "The specified category does not exist");

            Product product = new Product(
                command.Name,
                command.Description,
                command.ImageURL,
                command.Price,
                command.Quantity,
                command.IdCategory
            ); ;

            if (!product.IsValid)
                return new GenericCommandResult(false, "Correctly enter product data", product.Notifications);

            _productRepository.Add(product);

            return new GenericCommandResult(true, "Product created successfully", product);
        }
    }
}
