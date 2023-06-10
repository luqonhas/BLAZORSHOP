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
    public class SearchCartByUserIdQuery : Notifiable<Notification>, IQuery
    {
        public Guid IdUser { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotNull(IdUser, "idUser", "The 'idUser' field cannot be null!")
                );
        }

        public class ListProductByNameResult
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
