using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.Core.Data;
using NSE.Core.DomainObjects.Interfaces;

namespace NSE.Catalog.API.Models.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> ObterTodos();
        Task<Product> ObterPorId(Guid id);

        void Adicionar(Product produto);
        void Atualizar(Product produto);
    }
}
