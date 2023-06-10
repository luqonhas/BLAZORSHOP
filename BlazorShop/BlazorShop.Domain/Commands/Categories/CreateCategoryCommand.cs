using BlazorShop.Domain.Entities;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlazorShop.Domain.Commands.Categories
{
    public class CreateCategoryCommand : Notifiable<Notification>, ICommand
    {
        public CreateCategoryCommand(string name, string iconCSS)
        {
            Name = name;
            IconCSS = iconCSS;
        }

        public string Name { get; set; }
        public string IconCSS { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
            .IsNotEmpty(Name, "name", "The 'name' field cannot be empty!")
            .IsNotEmpty(IconCSS, "iconCSS", "The 'iconCSS' field cannot be empty!")
            );
        }
    }
}
