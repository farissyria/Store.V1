using AutoMapper;
using Microsoft.Extensions.Logging;
using Store.Application.DTOs;
using Store.Core.Entities;
using Store.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Store.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
            _logger = logger;
        }
        public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            
                if (productDto == null)
                    throw new ArgumentNullException(nameof(productDto));

                if (string.IsNullOrWhiteSpace(productDto.Name))
                    throw new ValidationException("Product name is required");

                if (productDto.Price <= 0)
                    throw new ValidationException("Product price must be greater than 0");

             
             
               var product = _mapper.Map<Product>(productDto);

           _unitOfWork.Products.Add(product);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("successfully created product");
            return _mapper.Map<ProductDto>(product);
             
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
           
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found", product);
                return false;
            }
            
            try {
                 product.IsActive = false;
                  _unitOfWork.Products.Delete(product);

                _logger.LogInformation("Deactivating product ID {ProductId}", product);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully deactivated product ID {ProductId}", product);
                return true;
            }
            
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unexpected error while deactivating product ID {ProductId}", id);
                throw; // Re-throw to let caller handle
            }

        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {           
            var product = await _unitOfWork.Products.GetAllAsync();
           return  _mapper.Map<IEnumerable<ProductDto>>(product);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
         var products = await _unitOfWork.Products.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(products);
        }

        public async Task UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id);

                if (product == null)
                {
                    _logger.LogError("Product with ID {ProductId} not found", id);
                   
                }

                _mapper.Map(productDto, product);
                product.UpdatedAt = DateTime.Now;

                _unitOfWork.Products.Update(product);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Successfully updated product ID: {ProductId}", id);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating product ID {ProductId}", id);
                throw; // Re-throw to let controller handle it
            }
        }
        public async Task<IEnumerable<ProductDto>> SearchProductAsync(string term)
        {
           var product=await _unitOfWork.Products.SearchAsync(term);
            if(product==null)
                _logger.LogError("Product with search is empty");
            return  _mapper.Map<IEnumerable<ProductDto>>(product);
        }
        

       
    }
}
