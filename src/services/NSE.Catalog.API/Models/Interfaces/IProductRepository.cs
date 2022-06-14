using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSE.Core.Data;

namespace NSE.Catalog.API.Models.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> FindAll();
        Task<Product> FindById(Guid id);
        void Add(Product produto);
        void Update(Product produto);
    }
}
