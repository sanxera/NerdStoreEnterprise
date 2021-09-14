using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NSE.Client.API.Application.Events;
using NSE.Client.API.Models.Interfaces;
using NSE.Core.Messages;

namespace NSE.Client.API.Application.Commands
{
    public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
    {
        private readonly IClientRepository _clientRepository;

        public ClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var client = new Models.Client(message.Id, message.Name, message.Email, message.Cpf);

            var clientExists = await _clientRepository.FindByCpf(client.Cpf.Number);

            if (clientExists != null)
            {
                AddError("Este CPF já está em uso.");
                return ValidationResult;
            }

            _clientRepository.Add(client);

            client.AddEvent(new RegisteredClientEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await SaveData(_clientRepository.UnitOfWork);
        }
    }
}
