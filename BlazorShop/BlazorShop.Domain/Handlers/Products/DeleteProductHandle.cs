using BlazorShop.Domain.Commands.Products;
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
    public class DeleteProductHandle : Notifiable<Notification>, IHandlerCommand<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandle(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ICommandResult Handler(DeleteProductCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly inform the product you want to delete", command.Notifications);
            }

            var searchedProduct = _productRepository.SearchById(command.Id);

            if (searchedProduct == null)
            {
                return new GenericCommandResult(false, "Product not found", command.Notifications);
            }

            _productRepository.Delete(searchedProduct.Id);

            return new GenericCommandResult(false, "Product deleted successfully!", "");
        }
    }
}
