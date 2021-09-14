using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> FindAll();

        Task<ProductViewModel> FindById(Guid id);
    }
}
