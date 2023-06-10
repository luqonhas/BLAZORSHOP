using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Commands.Categories
{
    public class DeleteCategoryCommand : Notifiable<Notification>, ICommand
    {
        public DeleteCategoryCommand() { }

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
