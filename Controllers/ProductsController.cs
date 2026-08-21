using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.Product;
using gb_prod_api.Mappers;
using gb_prod_api.Models;
using gb_prod_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gb_prod_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return ProductMapper.ToResponse(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return ProductMapper.ToResponse(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            var product = await _productService.UpdateProductAsync(id, updatedProduct);
            if (!product)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var isDeleted = await _productService.DeleteProductAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> CreateProduct(CreateProductRequest newProduct)
        {
            var createdProduct = await _productService.CreateProductAsync(newProduct);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, ProductMapper.ToResponse(createdProduct));
        }
    }
}