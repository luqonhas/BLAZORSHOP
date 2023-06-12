using BlazorShop.Shared.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace BlazorShop.Domain.Entities
{
    public class Product : Base
    {
        public Product(string name, string description, string imageURL, decimal price, int quantity, Guid idCategory)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(name, "name", "The 'name' field cannot be empty!")
                    .IsNotEmpty(description, "description", "The 'description' field cannot be empty!")
                    .IsNotNull(imageURL, "imageURL", "The 'imageURL' field cannot be null!")
                    .IsNotNull(price, "price", "The 'price' field cannot be null!")
                    .IsNotNull(quantity, "quantity", "The 'quantity' field cannot be null!")
                    .IsNotEmpty(idCategory, "idCategory", "The 'idCategory' field cannot be empty")
            );

            if (IsValid)
            {
                Name = name;
                Description = description;
                ImageURL = imageURL;
                Price = price;
                Quantity = quantity;
                ModifyDate = null;
                IdCategory = idCategory;
            }
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImageURL { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public DateTime? ModifyDate { get; private set; }

        // FK's
        public Guid IdCategory { get; set; }
        public Category Categories { get; set; }

        // Compositions
        public IReadOnlyCollection<ItemCart> ItemsCart { get; private set; }
        private List<ItemCart> _items;
    }
}
