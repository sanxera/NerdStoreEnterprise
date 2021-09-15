using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSE.Client.API.Application.Commands;
using NSE.Client.API.Application.Events;
using NSE.Client.API.Data;
using NSE.Client.API.Data.Repositorys;
using NSE.Client.API.Models.Interfaces;
using NSE.Client.API.Services;
using NSE.Core.Mediator;

namespace NSE.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();
            services.AddScoped<INotificationHandler<RegisteredClientEvent>, ClientEventHandler>();
            services.AddScoped<ClientContext>();

            services.AddHostedService<RegisteredClientIntegrationHandler>();
        }
    }
}
