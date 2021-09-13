using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalog.API.Models;
using NSE.Catalog.API.Models.Interfaces;

namespace NSE.Catalog.API.Controllers
{
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("catalog/products")]
        public async Task<IEnumerable<Product>> Index()
        {
            return await this._productRepository.FindAll();
        }

        [HttpGet("catalog/product/{id}")]
        public async Task<Product> ProductDetails(Guid id)
        {
            return await this._productRepository.FindById(id);
        }
    }
}
