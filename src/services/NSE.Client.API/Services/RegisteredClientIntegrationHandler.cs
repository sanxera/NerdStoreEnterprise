﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Client.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;

namespace NSE.Client.API.Services
{
    public class RegisteredClientIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisteredClientIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            _bus.Rpc.RespondAsync<RegisteredUserIntegrationEvent, ResponseMessage>(async request =>
                new ResponseMessage(await RegisterClient(request)));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> RegisterClient(RegisteredUserIntegrationEvent message)
        {
            var clientCommand = new RegisterClientCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult result;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                result = await mediator.SendCommand(clientCommand);
            }

            return result;
        }
    }
}