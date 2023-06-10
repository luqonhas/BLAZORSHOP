using BlazorShop.Domain.Entities;
using BlazorShop.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Commands.ItemsCart
{
    public class CreateItemCartCommand : Notifiable<Notification>, ICommand
    {
        public CreateItemCartCommand() { }

        public int Quantity { get; set; }

        // FK's
        public Guid IdCart { get; set; }
        public Guid IdProduct { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
            .IsNotNull(Quantity, "quantity", "The 'quantity' field cannot be null!")
            .IsNotNull(IdCart, "idCart", "The 'idCart' field cannot be null!")
            .IsNotNull(IdProduct, "idProduct", "The 'idProduct' field cannot be null!")
            );
        }
    }
}
