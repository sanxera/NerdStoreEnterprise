using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalog.API.Models;
using NSE.Catalog.API.Models.Interfaces;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Identity.ClaimsAuth;

namespace NSE.Catalog.API.Controllers
{
    [Authorize]
    public class CatalogController : MainController
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet("catalog/products")]
        public async Task<IEnumerable<Product>> Index()
        {
            return await this._productRepository.FindAll();
        }

        [ClaimsAuthorize("Catalog", "Read")]
        [HttpGet("catalog/product/{id}")]
        public async Task<Product> ProductDetails(Guid id)
        {
            return await this._productRepository.FindById(id);
        }
    }
}
