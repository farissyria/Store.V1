using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
        Task<IEnumerable<Product>> GetAvailableAsync();
        Task<bool> UpdateStockAsync(int productId, int quantity);

    }
}
