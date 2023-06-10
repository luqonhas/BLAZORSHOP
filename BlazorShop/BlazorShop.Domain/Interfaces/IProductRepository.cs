using BlazorShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        // Commands:
        void Add(Product product);

        void Delete(Guid? product);



        // Queries:
        IEnumerable<Product> List();

        Product SearchById(Guid? id);

        Product SearchByName(string name);
    }
}
