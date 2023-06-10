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
    public class DeleteProductCommand : Notifiable<Notification>, ICommand
    {
        public DeleteProductCommand() { }

        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                   .Requires()
                   .IsNotEmpty(Id, "Id", "The 'Id' field cannot be empty!")
            );
        }
    }
}
