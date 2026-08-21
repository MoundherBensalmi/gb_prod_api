using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.Data;
using gb_prod_api.DTOs.Product;
using gb_prod_api.Models;

namespace gb_prod_api.Services
{
    public class ProductService(AppDbContext context) : IProductService
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Product>> GetProductsAsync()
        {
            return _context.Products.ToList();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateProductAsync(CreateProductRequest product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        public async Task<bool> UpdateProductAsync(int id, Product updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}