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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private IRepository<Category> _categories;
        private IProductRepository _products;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }
        public IRepository<Category> Categories =>
            _categories ??= new Repository<Category>(_context);
        public IProductRepository Products =>
            _products ??= new ProductRepository(_context);
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
