using BlazorShop.Domain.Entities;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Queries.Carts
{
    public class SearchCartByIdQuery : Notifiable<Notification>, IQuery
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(Id, "Id", "The 'Id' field cannot be empty!")
                );
        }

        public class SearchCartByIdResult
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string IconCSS { get; set; }
            public DateTime? ModifyDate { get; set; }

            // FK's
            public Guid IdUser { get; set; }

            // Compositions
            public IReadOnlyCollection<ItemCart> ItemsCart { get; set; }
            private List<ItemCart> _items { get; set; }

        }
    }
}
