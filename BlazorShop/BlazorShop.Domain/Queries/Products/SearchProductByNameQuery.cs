using BlazorShop.Domain.Entities;
using BlazorShop.Shared.Enums;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Queries.Products
{
    public class SearchProductByNameQuery : Notifiable<Notification>, IQuery
    {
        public string Name { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(Name, "Name", "The 'Name' field cannot be empty")
                );
        }

        public class ListProductByNameResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageURL { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public DateTime? ModifyDate { get; set; }

            // FK's
            public Guid IdCategory { get; set; }

            // Compositions
            public IReadOnlyCollection<ItemCart> ItemsCart { get; set; }
            private List<ItemCart> _items { get; set; }
        }
    }
}
