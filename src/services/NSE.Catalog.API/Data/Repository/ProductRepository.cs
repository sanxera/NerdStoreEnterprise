using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Models;
using NSE.Catalog.API.Models.Interfaces;
using NSE.Core.Data;

namespace NSE.Catalog.API.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Product>> ObterTodos()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> ObterPorId(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Adicionar(Product Product)
        {
            _context.Products.Add(Product);
        }

        public void Atualizar(Product Product)
        {
            _context.Products.Update(Product);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
