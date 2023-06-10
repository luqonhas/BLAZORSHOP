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

        public CreateProductHandle(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ICommandResult Handler(CreateProductCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Correctly enter product data", command.Notifications);

            Product product = new Product(
                command.Name,
                command.Description,
                command.ImageURL,
                command.Price,
                command.Quantity
            );

            if (!product.IsValid)
                return new GenericCommandResult(false, "Correctly enter product data", product.Notifications);

            _productRepository.Add(product);

            return new GenericCommandResult(true, "Product created successfully", product);
        }
    }
}
