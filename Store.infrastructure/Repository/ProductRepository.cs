using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAvailableAsync()
        {
            return await _dbSet
                .Where(p => p.StockQuantity > 0 && p.IsActive)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            var product = await GetByIdAsync(productId);
            if (product == null) 
                return false;

            product.StockQuantity = quantity;
            product.UpdatedAt = DateTime.UtcNow;
            _context.Update(product);
            return true;
        }
    }
}
