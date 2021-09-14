using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.Core.Messages;

namespace NSE.Client.API.Application.Events
{
    public class RegisteredClientEvent : Event
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisteredClientEvent(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }
    }
}
