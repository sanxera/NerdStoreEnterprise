using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSE.Client.API.Models.Interfaces;
using NSE.Core.Data;

namespace NSE.Client.API.Data.Repositorys
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _context;

        public ClientRepository(ClientContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Models.Client>> FindAll()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public Task<Models.Client> FindByCpf(string cpf)
        {
            return _context.Clients.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public void Add(Models.Client clients)
        {
            _context.Clients.Add(clients);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
