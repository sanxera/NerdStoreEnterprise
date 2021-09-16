using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Client.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;

namespace NSE.Client.API.Services
{
    public class RegisteredClientIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisteredClientIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();

            return Task.CompletedTask;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<RegisteredUserIntegrationEvent, ResponseMessage>(async request =>
                await RegisterClient(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegisterClient(RegisteredUserIntegrationEvent message)
        {
            var clientCommand = new RegisterClientCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult result;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                result = await mediator.SendCommand(clientCommand);
            }

            return new ResponseMessage(result);
        }
    }
}
