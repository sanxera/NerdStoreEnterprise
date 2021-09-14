using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace NSE.Client.API.Application.Events
{
    public class ClientEventHandler : INotificationHandler<RegisteredClientEvent>
    {
        public Task Handle(RegisteredClientEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
