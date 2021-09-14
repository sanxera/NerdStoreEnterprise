using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;

namespace NSE.Client.API.Application.Commands
{
    public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var cliente = new Models.Client(message.Id, message.Name, message.Email, message.Cpf);

            ////var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Number);

            ////if (clienteExistente != null)
            ////{
            ////    AddError("Este CPF já está em uso.");
            ////    return ValidationResult;
            ////}

            throw new NotImplementedException();
        }
    }
}
