using System;
using NSE.Core.Messages;

namespace NSE.Client.API.Application.Commands
{
    public class RegisterClientCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisterClientCommand(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }


    }
}
