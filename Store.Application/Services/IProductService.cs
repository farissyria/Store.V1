using Store.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
        Task UpdateProductAsync(int id, UpdateProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> SearchProductAsync(string term);
        //Task<bool> UpadteStockQuantity(int prId, int quantity);
    }
}
