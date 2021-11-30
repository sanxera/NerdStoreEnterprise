using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.BFF.Purchases.Models;

namespace NSE.BFF.Purchases.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ItemProductDto>> FindAll();

        Task<ItemProductDto> FindById(Guid id);
    }
}
