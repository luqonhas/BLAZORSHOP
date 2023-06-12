using BlazorShop.Domain.Entities;
using BlazorShop.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Commands.Products
{
    public class CreateProductCommand : Notifiable<Notification>, ICommand
    {
        public CreateProductCommand() { }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // FK's
        public Guid IdCategory { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
                    .IsNotEmpty(Name, "name", "The 'name' field cannot be empty!")
                    .IsNotEmpty(Description, "description", "The 'description' field cannot be empty!")
                    .IsNotNull(ImageURL, "imageURL", "The 'imageURL' field cannot be null!")
                    .IsNotNull(Price, "price", "The 'price' field cannot be null!")
                    .IsNotNull(Quantity, "quantity", "The 'quantity' field cannot be null!")
                    .IsNotNull(IdCategory, "idCategory", "The 'idCategory' field cannot be null!")
            );
        }
    }
}
