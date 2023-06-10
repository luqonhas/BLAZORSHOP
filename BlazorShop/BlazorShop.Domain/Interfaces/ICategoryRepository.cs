using BlazorShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        // Commands:
        void Add(Category category);

        void Delete(Guid? category);



        // Queries:
        IEnumerable<Category> List();
        Category SearchById(Guid? id);
    }
}
