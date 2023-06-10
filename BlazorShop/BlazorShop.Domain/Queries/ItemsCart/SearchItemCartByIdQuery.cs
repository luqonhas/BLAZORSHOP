using BlazorShop.Domain.Entities;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Queries.ItemsCart
{
    public class SearchItemCartByIdQuery : Notifiable<Notification>, IQuery
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

        public class SearchItemCartByIdResult
        {
            public Guid Id { get; set; }
            public int Quantity { get; private set; }
            public DateTime? ModifyDate { get; private set; }

            // FK's
            public Guid IdCart { get; set; }

            // FK's
            public Guid IdProduct { get; set; }
        }
    }
}
