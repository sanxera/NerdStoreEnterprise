using System.Collections.Generic;
using System.Threading.Tasks;
using NSE.Core.Data;

namespace NSE.Client.API.Models.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        void Add(Client client);
        Task<IEnumerable<Client>> FindAll();
        Task<Client> FindByCpf(string cpf);
    }
}
