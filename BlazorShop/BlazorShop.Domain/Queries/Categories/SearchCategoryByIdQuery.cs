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

namespace BlazorShop.Domain.Queries.Categories
{
    public class SearchCategoryByIdQuery : Notifiable<Notification>, IQuery
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

        public class SearchCategoryByIdResult
        {
            public Guid Id { get; set; }
            public string Name { get; private set; }
            public string IconCSS { get; private set; }
            public DateTime? ModifyDate { get; private set; }

            // Compositions
            public IReadOnlyCollection<Product> Products { get; private set; }
            private List<Product> _products { get; set; }
        }
    }
}
