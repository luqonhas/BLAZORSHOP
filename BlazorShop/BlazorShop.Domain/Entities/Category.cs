using BlazorShop.Shared.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Entities
{
    public class Category : Base
    {
        public Category(string name, string iconCSS)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(name, "name", "The 'name' field cannot be empty!")
                    .IsNotEmpty(iconCSS, "iconCSS", "The 'iconCSS' field cannot be empty!")
            );

            if (IsValid)
            {
                Name = name;
                IconCSS = iconCSS;
                ModifyDate = null;
            }
        }

        public string Name { get; private set; }
        public string IconCSS { get; private set; }
        public DateTime? ModifyDate { get; private set; }

        // Compositions
        public IReadOnlyCollection<Product> Products { get; private set; }
        private List<Product> _products { get; set; }
    }
}
